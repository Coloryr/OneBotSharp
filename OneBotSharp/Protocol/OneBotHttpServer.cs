using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneBotSharp.Objs.Event;
using HttpMethod = DotNetty.Codecs.Http.HttpMethod;
using HttpVersion = DotNetty.Codecs.Http.HttpVersion;

namespace OneBotSharp.Protocol;

public class OneBotHttpServer : IOneBot<IRecvServer>, IRecvServer
{
    private readonly IEventLoopGroup _group;
    private readonly IEventLoopGroup _workGroup;
    private readonly ServerBootstrap _bootstrap;
    private IChannel _bootstrapChannel;
    private readonly bool _haveKey;
    private readonly HMACSHA1 _hMACSHA1;

    public override IRecvServer Pipe => this;

    public event Action<EventBase>? EventRecv;

    public OneBotHttpServer(string url, string? key = null) : base(url, key)
    {
        if (Url == null)
        {
            throw new ArgumentNullException(nameof(Url), "Url is null");
        }

        if (!string.IsNullOrWhiteSpace(Key))
        {
            _haveKey = true;
            _hMACSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(Key));
        }

        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        var useLibuv = RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    || RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        if (useLibuv)
        {
            var dispatcher = new DispatcherEventLoopGroup();
            _group = dispatcher;
            _workGroup = new WorkerEventLoopGroup(dispatcher);
        }
        else
        {
            _group = new MultithreadEventLoopGroup(1);
            _workGroup = new MultithreadEventLoopGroup();
        }

        _bootstrap = new();
        _bootstrap.Group(_group, _workGroup);

        if (useLibuv)
        {
            _bootstrap.Channel<TcpServerChannel>()
                .Option(ChannelOption.SoReuseport, true)
                .ChildOption(ChannelOption.SoReuseaddr, true);
        }
        else
        {
            _bootstrap.Channel<TcpServerSocketChannel>();
        }

        _bootstrap
            .Option(ChannelOption.SoBacklog, 8192)
            .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
            {
                IChannelPipeline pipeline = channel.Pipeline;
                pipeline.AddLast(new ReadTimeoutHandler(Timeout));
                pipeline.AddLast(new WriteTimeoutHandler(Timeout));
                pipeline.AddLast("codec", new HttpServerCodec(4096, 8192, 8192, false));
                pipeline.AddLast("agg", new HttpObjectAggregator(65536));
                pipeline.AddLast("handler", new OneBotServerHandler(this));
            }));
    }

    public override void Dispose()
    {
        if (_haveKey)
        {
            _hMACSHA1.Clear();
        }
        _group.ShutdownGracefullyAsync().Wait();
    }

    public override async Task Start()
    {
        var uri = new Uri(Url);
        _bootstrapChannel = await _bootstrap.BindAsync(new IPEndPoint(IPAddress.Parse(uri.Host), uri.Port));
    }

    public override Task Close()
    {
        if (_bootstrapChannel != null)
        {
            return _bootstrapChannel.CloseAsync();
        }

        return Task.CompletedTask;
    }

    private class OneBotServerHandler(OneBotHttpServer bot) : ChannelHandlerAdapter
    {
        static readonly AsciiString TypeJson = AsciiString.Cached("application/json");
        static readonly AsciiString ServerName = AsciiString.Cached("OntBotSharp");
        static readonly AsciiString ContentTypeEntity = HttpHeaderNames.ContentType;
        static readonly AsciiString ContentLengthEntity = HttpHeaderNames.ContentLength;
        static readonly AsciiString ServerEntity = HttpHeaderNames.Server;
        static readonly AsciiString Signature = AsciiString.Cached("x-signature");

        public override void ChannelRead(IChannelHandlerContext ctx, object message)
        {
            if (message is IHttpRequest request)
            {
                try
                {
                    Process(ctx, request);
                }
                finally
                {
                    ReferenceCountUtil.Release(message);
                }
            }
            else
            {
                ctx.FireChannelRead(message);
            }
        }

        void Process(IChannelHandlerContext ctx, IHttpRequest request)
        {
            if (request.Method == HttpMethod.Post && request is IFullHttpRequest full)
            {
                var content = full.Content;
                if (content.IsReadable())
                {
                    var temp = new byte[content.ReadableBytes];
                    content.ReadBytes(temp);

                    if (bot._haveKey)
                    {
                        byte[] hash = bot._hMACSHA1.ComputeHash(temp);
                        string sig = BitConverter.ToString(hash).Replace("-", "").ToLower();

                        if (!request.Headers.TryGetAsString(Signature, out var receivedSig)
                            || $"sha1={sig}" != receivedSig)
                        {
                            var response = new DefaultFullHttpResponse(HttpVersion.Http11,
                                HttpResponseStatus.Forbidden, Unpooled.Empty, false);
                            ctx.WriteAndFlushAsync(response);
                            ctx.CloseAsync();
                        }
                    }

                    string postContent = Encoding.UTF8.GetString(temp);

                    JObject obj;
                    try
                    {
                        obj = JObject.Parse(postContent);
                    }
                    catch (Exception e)
                    {
                        var response = new DefaultFullHttpResponse(HttpVersion.Http11,
                            HttpResponseStatus.BadRequest, Unpooled.Empty, false);
                        ctx.WriteAndFlushAsync(response);
                        ctx.CloseAsync();
                        Console.WriteLine(e);
                        return;
                    }
                    try
                    {
                        var eb = EventBase.ParseRecv(obj);
                        if (eb == null)
                        {
                            var response = new DefaultFullHttpResponse(HttpVersion.Http11,
                                HttpResponseStatus.BadRequest, Unpooled.Empty, false);
                            ctx.WriteAndFlushAsync(response);
                            ctx.CloseAsync();
                            return;
                        }

                        bot.EventRecv?.Invoke(eb);

                        if (eb.Reply != null)
                        {
                            string temp1 = JsonConvert.SerializeObject(eb.Reply);
                            byte[] json = Encoding.UTF8.GetBytes(temp1);

                            var length = new AsciiString($"{json.Length}");
                            IByteBuffer buffer = Unpooled.WrappedBuffer(json);
                            var response = new DefaultFullHttpResponse(HttpVersion.Http11,
                                HttpResponseStatus.OK, buffer, false);
                            HttpHeaders headers = response.Headers;
                            headers.Set(ContentTypeEntity, TypeJson);
                            headers.Set(ServerEntity, ServerName);
                            headers.Set(ContentLengthEntity, length);

                            ctx.WriteAndFlushAsync(response);
                            ctx.CloseAsync();
                            return;
                        }
                    }
                    catch (Exception e)
                    {
                        var response = new DefaultFullHttpResponse(HttpVersion.Http11,
                                HttpResponseStatus.InternalServerError, Unpooled.Empty, false);
                        ctx.WriteAndFlushAsync(response);
                        ctx.CloseAsync();
                        Console.WriteLine(e);
                        return;
                    }
                }

                {
                    var response = new DefaultFullHttpResponse(HttpVersion.Http11,
                        HttpResponseStatus.NoContent, Unpooled.Empty, false);
                    ctx.WriteAndFlushAsync(response);
                    ctx.CloseAsync();
                }
            }
            else
            {
                var response = new DefaultFullHttpResponse(HttpVersion.Http11,
                    HttpResponseStatus.NotFound, Unpooled.Empty, false);
                ctx.WriteAndFlushAsync(response);
                ctx.CloseAsync();
            }
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) => context.CloseAsync();

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void UserEventTriggered(IChannelHandlerContext context, object evt)
        {
            if (evt is ReadTimeoutException or WriteTimeoutException)
            {
                Console.WriteLine("Timeout occurred, closing connection.");
                context.CloseAsync();
            }
            else
            {
                base.UserEventTriggered(context, evt);
            }
        }
    }
}
