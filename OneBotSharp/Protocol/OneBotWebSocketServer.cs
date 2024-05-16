using System.Collections.Concurrent;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;
using Newtonsoft.Json.Linq;
using OneBotSharp.Objs.Api;
using OneBotSharp.Objs.Event;
using HttpVersion = DotNetty.Codecs.Http.HttpVersion;

namespace OneBotSharp.Protocol;

public class OneBotWebSocketServer : IOneBot<ISendRecvPipeServer>, ISendRecvPipeServer
{
    private IEventLoopGroup bossGroup;
    private IEventLoopGroup workGroup;
    private IChannel _bootstrapChannel;
    private ServerBootstrap bootstrap;
    private Uri uri;
    private ConcurrentDictionary<IChannel, ClientPipe> _clients = [];

    public override ISendRecvPipeServer Pipe => this;

    public event Action<ISendClient, EventBase>? EventRecv;

    public OneBotWebSocketServer(string url, string? key) : base(url, key)
    {
        if (Url == null)
        {
            throw new ArgumentNullException(nameof(Url), "Url is null");
        }

        uri = new Uri(Url);

        var useLibuv = RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    || RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        if (useLibuv)
        {
            var dispatcher = new DispatcherEventLoopGroup();
            bossGroup = dispatcher;
            workGroup = new WorkerEventLoopGroup(dispatcher);
        }
        else
        {
            bossGroup = new MultithreadEventLoopGroup(1);
            workGroup = new MultithreadEventLoopGroup();
        }

        bootstrap = new ServerBootstrap();
        bootstrap.Group(bossGroup, workGroup);

        if (useLibuv)
        {
            bootstrap.Channel<TcpServerChannel>();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                bootstrap
                    .Option(ChannelOption.SoReuseport, true)
                    .ChildOption(ChannelOption.SoReuseaddr, true);
            }
        }
        else
        {
            bootstrap.Channel<TcpServerSocketChannel>();
        }

        bootstrap
            .Option(ChannelOption.SoBacklog, 8192)
            .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
            {
                IChannelPipeline pipeline = channel.Pipeline;
                pipeline.AddLast(new HttpServerCodec());
                pipeline.AddLast(new HttpObjectAggregator(65536));
                pipeline.AddLast(new WebSocketServerHandler(this));
            }));
    }

    public override async Task Start()
    {
        _bootstrapChannel = await bootstrap.BindAsync(new IPEndPoint(IPAddress.Parse(uri.Host), uri.Port));
    }

    public override Task Close()
    {
        if (_bootstrapChannel != null)
        {
            return _bootstrapChannel.CloseAsync();
        }

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        workGroup.ShutdownGracefullyAsync().Wait();
        bossGroup.ShutdownGracefullyAsync().Wait();
    }

    private class ClientPipe(IChannel ch, WebSocketServerHandshaker handel, OneBotWebSocketServer bot) : ISendClient
    {
        private ConcurrentDictionary<string, (Semaphore, JToken?)> _queues = [];

        private async void Send(string data)
        {
            WebSocketFrame frame = new TextWebSocketFrame(data);
            await ch.WriteAndFlushAsync(frame);
        }

        private async Task<T?> Send<T>(string url, object? data = null)
        {
            string uuid;
            do
            {
                uuid = Guid.NewGuid().ToString().ToLower();
            }
            while (_queues.ContainsKey(uuid));
            var obj = new JObject
            {
                { "action", url[1..] },
                { "params", data == null ? null : JToken.FromObject(data) },
                { "echo", uuid }
            };
            using var sem = new Semaphore(0, 2);
            _queues.TryAdd(uuid, (sem, null));
            Send(obj.ToString());
            await Task.Run(sem.WaitOne);
            _queues.Remove(uuid, out var data1);
            if (data1.Item2 is { } obj1)
            {
                return obj1.ToObject<T>();
            }
            else
            {
                return default;
            }
        }

        public async Task<bool?> CanSendImage()
        {
            var obj = await Send<JObject>(SendUrl.CanSendImage);
            return obj?["yes"]?.Value<bool>();
        }

        public async Task<bool?> CanSendRecord()
        {
            var obj = await Send<JObject>(SendUrl.CanSendRecord);
            return obj?["yes"]?.Value<bool>();
        }

        public Task CleanCache()
        {
            return Send<object>(SendUrl.CleanCache);
        }

        public Task<DeleteMsgRes?> DeleteMsg(DeleteMsg msg)
        {
            return Send<DeleteMsgRes>(SendUrl.DeleteMsg, msg);
        }

        public Task<GetCookiesRes?> GetCookies(GetCookies msg)
        {
            return Send<GetCookiesRes>(SendUrl.GetCookies, msg);
        }

        public Task<GetCredentialsRes?> GetCredentials(GetCredentials msg)
        {
            return Send<GetCredentialsRes>(SendUrl.GetCredentials, msg);
        }

        public Task<GetCsrfTokenRes?> GetCsrfToken()
        {
            return Send<GetCsrfTokenRes>(SendUrl.GetCsrfToken);
        }

        public async Task<GetForwardMsgRes?> GetForwardMsg(GetForwardMsg msg)
        {
            var res = await Send<GetForwardMsgRes>(SendUrl.GetForwardMsg, msg);
            if (res is { })
            {
                res.Parse();
            }

            return res;
        }

        public Task<List<GetFriendListRes>?> GetFriendList()
        {
            return Send<List<GetFriendListRes>>(SendUrl.GetFriendList);
        }

        public Task<GetGroupHonorInfoRes?> GetGroupHonorInfo(GetGroupHonorInfo msg)
        {
            return Send<GetGroupHonorInfoRes>(SendUrl.GetGroupHonorInfo, msg);
        }

        public Task<GetGroupInfoRes?> GetGroupInfo(GetGroupInfo msg)
        {
            return Send<GetGroupInfoRes>(SendUrl.GetGroupInfo, msg);
        }

        public Task<List<GetGroupListRes>?> GetGroupList()
        {
            return Send<List<GetGroupListRes>>(SendUrl.GetGroupList);
        }

        public Task<GetGroupMemberInfoRes?> GetGroupMemberInfo(GetGroupMemberInfo msg)
        {
            return Send<GetGroupMemberInfoRes>(SendUrl.GetGroupMemberInfo, msg);
        }

        public Task<GetGroupMemberListRes?> GetGroupMemberList(GetGroupMemberList msg)
        {
            return Send<GetGroupMemberListRes>(SendUrl.GetGroupMemberList, msg);
        }

        public Task<GetImageRes?> GetImage(GetImage msg)
        {
            return Send<GetImageRes>(SendUrl.GetImage, msg);
        }

        public Task<GetLoginInfoRes?> GetLoginInfo()
        {
            return Send<GetLoginInfoRes>(SendUrl.GetLoginInfo);
        }

        public async Task<GetMsgRes?> GetMsg(GetMsg msg)
        {
            var res = await Send<GetMsgRes>(SendUrl.GetMsg, msg);
            if (res is { })
            {
                res.Parse();
            }
            return res;
        }

        public Task<GetRecordRes?> GetRecord(GetRecord msg)
        {
            return Send<GetRecordRes>(SendUrl.GetRecord, msg);
        }

        public Task<GetStatusRes?> GetStatus()
        {
            return Send<GetStatusRes>(SendUrl.GetStatus);
        }

        public Task<GetStrangerInfoRes?> GetStrangerInfo(GetStrangerInfo msg)
        {
            return Send<GetStrangerInfoRes>(SendUrl.GetStrangerInfo, msg);
        }

        public Task<GetVersionInfoRes?> GetVersionInfo()
        {
            return Send<GetVersionInfoRes>(SendUrl.GetVersionInfo);
        }

        public Task<SendGroupMsgRes?> SendGroupMsg(SendGroupMsg msg)
        {
            return Send<SendGroupMsgRes>(SendUrl.SendGroupMsg, msg);
        }

        public Task<SendLikeRes?> SendLike(SendLike msg)
        {
            return Send<SendLikeRes>(SendUrl.SendLike, msg);
        }

        public Task<SendMsgRes?> SendMsg(SendMsg msg)
        {
            return Send<SendMsgRes>(SendUrl.SendMsg, msg);
        }

        public Task<SendPrivateMsgRes?> SendPrivateMsg(SendPrivateMsg msg)
        {
            return Send<SendPrivateMsgRes>(SendUrl.SendPrivateMsg, msg);
        }

        public Task<SetFriendAddRequestRes?> SetFriendAddRequest(SetFriendAddRequest msg)
        {
            return Send<SetFriendAddRequestRes>(SendUrl.SetFriendAddRequest, msg);
        }

        public Task<SetGroupAddRequestRes?> SetGroupAddRequest(SetGroupAddRequest msg)
        {
            return Send<SetGroupAddRequestRes>(SendUrl.SetGroupAddRequest, msg);
        }

        public Task<SetGroupAdminRes?> SetGroupAdmin(SetGroupAdmin msg)
        {
            return Send<SetGroupAdminRes>(SendUrl.SetGroupAdmin, msg);
        }

        public Task<SetGroupAnonymousRes?> SetGroupAnonymous(SetGroupAnonymous msg)
        {
            return Send<SetGroupAnonymousRes>(SendUrl.SetGroupAnonymous, msg);
        }

        public Task<SetGroupAnonymousBanRes?> SetGroupAnonymousBan(SetGroupAnonymousBan msg)
        {
            return Send<SetGroupAnonymousBanRes>(SendUrl.SetGroupAnonymousBan, msg);
        }

        public Task<SetGroupBanRes?> SetGroupBan(SetGroupBan msg)
        {
            return Send<SetGroupBanRes>(SendUrl.SetGroupBan, msg);
        }

        public Task<SetGroupCardRes?> SetGroupCard(SetGroupCard msg)
        {
            return Send<SetGroupCardRes>(SendUrl.SetGroupCard, msg);
        }

        public Task<SetGroupKickRes?> SetGroupKick(SetGroupKick msg)
        {
            return Send<SetGroupKickRes>(SendUrl.SetGroupKick, msg);
        }

        public Task<SetGroupLeaveRes?> SetGroupLeave(SetGroupLeave msg)
        {
            return Send<SetGroupLeaveRes>(SendUrl.SetGroupLeave, msg);
        }

        public Task<SetGroupNameRes?> SetGroupName(SetGroupName msg)
        {
            return Send<SetGroupNameRes>(SendUrl.SetGroupName, msg);
        }

        public Task<SetGroupSpecialTitleRes?> SetGroupSpecialTitle(SetGroupSpecialTitle msg)
        {
            return Send<SetGroupSpecialTitleRes>(SendUrl.SetGroupSpecialTitle, msg);
        }

        public Task<SetGroupWholeBanRes?> SetGroupWholeBan(SetGroupWholeBan msg)
        {
            return Send<SetGroupWholeBanRes>(SendUrl.SetGroupWholeBan, msg);
        }

        public Task<SetRestartRes?> SetRestart(SetRestart msg)
        {
            return Send<SetRestartRes>(SendUrl.SetRestart, msg);
        }

        internal void Stop()
        {
            handel.CloseAsync(ch, null);
        }

        internal void Close(CloseWebSocketFrame closeWebSocketFrame)
        {
            handel.CloseAsync(ch, closeWebSocketFrame);
        }

        internal void Text(TextWebSocketFrame textFrame)
        {
            string text = textFrame.Text();
            var obj = JObject.Parse(text);
            if (obj.TryGetValue("echo", out var echo)
                && _queues.TryGetValue(echo.ToString(), out var handel))
            {
                var status = obj["status"]?.ToString();
                if (status == "ok")
                {
                    _queues["echo"] = (handel.Item1, obj["data"]);
                }

                handel.Item1.Release();
            }
            else if (obj.ContainsKey("self_id"))
            {
                var ev = EventBase.ParseRecv(obj);
                if (ev != null)
                {
                    bot.EventRecv?.Invoke(this, ev);
                }
            }
        }
    }

    public sealed class WebSocketServerHandler(OneBotWebSocketServer bot) : SimpleChannelInboundHandler<object>
    {
        private readonly WebSocketServerHandshakerFactory wsFactory = new(bot.Url, null, true, 5 * 1024 * 1024);

        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {
            if (msg is IFullHttpRequest req)
            {
                // Handshake
                var handshaker = wsFactory.NewHandshaker(req);
                if (handshaker == null)
                {
                    WebSocketServerHandshakerFactory.SendUnsupportedVersionResponse(ctx.Channel);
                    var response = new DefaultFullHttpResponse(HttpVersion.Http11,
                       HttpResponseStatus.Forbidden, Unpooled.Empty, false);
                    ctx.WriteAndFlushAsync(response);
                    ctx.CloseAsync();
                }
                else
                {
                    bot._clients.TryAdd(ctx.Channel, new(ctx.Channel, handshaker, bot));
                    handshaker.HandshakeAsync(ctx.Channel, req);
                }

                return;
            }
            else if (msg is WebSocketFrame frame)
            {
                HandleWebSocketFrame(ctx, frame);
            }
        }

        public override void ChannelUnregistered(IChannelHandlerContext ctx)
        {
            base.ChannelUnregistered(ctx);
            if (bot._clients.TryGetValue(ctx.Channel, out var handshaker))
            {
                handshaker.Stop();
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        void HandleWebSocketFrame(IChannelHandlerContext ctx, WebSocketFrame frame)
        {
            // Check for closing frame
            if (frame is CloseWebSocketFrame)
            {
                if (bot._clients.TryGetValue(ctx.Channel, out var handshaker))
                {
                    handshaker.Close((CloseWebSocketFrame)frame.Retain());
                }
                return;
            }

            if (frame is PingWebSocketFrame)
            {
                ctx.WriteAsync(new PongWebSocketFrame((IByteBuffer)frame.Content.Retain()));
                return;
            }

            if (frame is TextWebSocketFrame textFrame)
            {
                if (bot._clients.TryGetValue(ctx.Channel, out var handel))
                {
                    handel.Text(textFrame);
                }
                return;
            }

            if (frame is BinaryWebSocketFrame)
            {

                return;
            }
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            Console.WriteLine($"{nameof(WebSocketServerHandler)} {0}", e);
            ctx.CloseAsync();
        }
    }
}
