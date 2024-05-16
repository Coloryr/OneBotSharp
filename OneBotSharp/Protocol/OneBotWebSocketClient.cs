using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;
using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Codecs.Http.WebSockets.Extensions.Compression;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;
using Newtonsoft.Json.Linq;
using OneBotSharp.Objs.Api;
using OneBotSharp.Objs.Event;

namespace OneBotSharp.Protocol;

public class OneBotWebSocketClient : IOneBot<ISendRecvPipe>, ISendRecvPipe
{
    private IEventLoopGroup _group;
    private IChannel _ch;
    private Bootstrap _bootstrap;
    private Uri _uri;
    private ConcurrentDictionary<string, (Semaphore, JToken?)> _queues = [];

    public override ISendRecvPipe Pipe => this;

    public event Action<ISendRecvPipe, EventBase>? EventRecv;
    public event Action<ISendRecvPipe, ISendRecvPipe.PipeState>? StateChange;

    public OneBotWebSocketClient(string url, string? key = null) : base(url, key)
    {
        if (Url == null)
        {
            throw new ArgumentNullException(nameof(Url), "Url is null");
        }

        _uri = new Uri(Url);

        var useLibuv = RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    || RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        if (useLibuv)
        {
            _group = new EventLoopGroup();
        }
        else
        {
            _group = new MultithreadEventLoopGroup();
        }

        _bootstrap = new Bootstrap();
        _bootstrap
            .Group(_group)
            .Option(ChannelOption.TcpNodelay, true);

        if (useLibuv)
        {
            _bootstrap.Channel<TcpChannel>();
        }
        else
        {
            _bootstrap.Channel<TcpSocketChannel>();
        }

        // Connect with V13 (RFC 6455 aka HyBi-17). You can change it to V08 or V00.
        // If you change it to V00, ping is not supported and remember to change
        // HttpResponseDecoder to WebSocketHttpResponseDecoder in the pipeline.
        var handler = new WebSocketClientHandler(this,
            WebSocketClientHandshakerFactory.NewHandshaker(
                    _uri, WebSocketVersion.V13, null, true, new DefaultHttpHeaders()));

        _bootstrap.Handler(new ActionChannelInitializer<IChannel>(channel =>
        {
            IChannelPipeline pipeline = channel.Pipeline;
            if (Timeout is { } time)
            {
                pipeline.AddLast(new ReadTimeoutHandler(time));
                pipeline.AddLast(new WriteTimeoutHandler(time));
            }
            pipeline.AddLast(
                new HttpClientCodec(),
                new HttpObjectAggregator(8192),
                WebSocketClientCompressionHandler.Instance,
                handler);
        }));
    }

    public override async Task Start()
    {
        _ch = await _bootstrap.ConnectAsync(_uri.Host, _uri.Port);
    }

    public override Task Close()
    {
        if (_ch != null)
        {
            return _ch.CloseAsync();
        }

        return Task.CompletedTask;
    }

    private async void Send(string data)
    {
        WebSocketFrame frame = new TextWebSocketFrame(data);
        await _ch.WriteAndFlushAsync(frame);
    }

    private async Task<T?> Send<T>(string url, object? data = null)
    {
        if (_ch == null || !_ch.IsWritable)
        {
            throw new Exception("websocket is not connetc");
        }
        string uuid;
        do
        {
            uuid = Guid.NewGuid().ToString().ToLower();
        }
        while (_queues.ContainsKey(uuid));
        var obj = new JObject
        {
            { "action", url },
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

    public async void Ping()
    {
        if (_ch?.IsWritable == true)
        {
            var frame = new PingWebSocketFrame();
            await _ch.WriteAndFlushAsync(frame);
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

    public override async void Dispose()
    {
        if (_ch?.IsWritable == true)
        {
            await _ch.WriteAndFlushAsync(new CloseWebSocketFrame());
        }

        await _group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
    }

    public class WebSocketClientHandler(OneBotWebSocketClient bot, WebSocketClientHandshaker handshaker)
        : SimpleChannelInboundHandler<object>
    {
        public override bool IsSharable => true;

        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            handshaker.HandshakeAsync(ctx.Channel);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            Console.WriteLine("WebSocket Client disconnected!");
            bot.StateChange?.Invoke(bot, ISendRecvPipe.PipeState.Disconnected);
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {
            IChannel ch = ctx.Channel;
            if (!handshaker.IsHandshakeComplete)
            {
                try
                {
                    handshaker.FinishHandshake(ch, (IFullHttpResponse)msg);
                    bot.StateChange?.Invoke(bot, ISendRecvPipe.PipeState.Connected);
                    Console.WriteLine("WebSocket Client connected!");
                }
                catch (WebSocketHandshakeException e)
                {
                    Console.WriteLine("WebSocket Client failed to connect");
                    bot.StateChange?.Invoke(bot, ISendRecvPipe.PipeState.ConnectFail);
                    Console.WriteLine(e);
                }

                return;
            }

            if (msg is IFullHttpResponse response)
            {
                throw new InvalidOperationException(
                    $"Unexpected FullHttpResponse (getStatus={response.Status}, content={response.Content.ToString(Encoding.UTF8)})");
            }

            if (msg is TextWebSocketFrame textFrame)
            {
                string text = textFrame.Text();
                var obj = JObject.Parse(text);
                if (obj.TryGetValue("echo", out var echo)
                    && bot._queues.TryGetValue(echo.ToString(), out var handel))
                {
                    var status = obj["status"]?.ToString();
                    if (status == "ok")
                    {
                        bot._queues[echo.ToString()] = (handel.Item1, obj["data"]);
                    }

                    handel.Item1.Release();
                }
                else if (obj.ContainsKey("self_id"))
                {
                    var ev = EventBase.ParseRecv(obj);
                    if (ev != null)
                    {
                        bot.EventRecv?.Invoke(bot, ev);
                    }
                }
            }
            else if (msg is PongWebSocketFrame)
            {
                Console.WriteLine("WebSocket Client received pong");
            }
            else if (msg is CloseWebSocketFrame)
            {
                Console.WriteLine("WebSocket Client received closing");
                ch.CloseAsync();
            }
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            ctx.CloseAsync();
        }

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
