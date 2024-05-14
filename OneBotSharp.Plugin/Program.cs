using OneBotSharp.Objs.Api;
using OneBotSharp.Objs.Event;
using OneBotSharp.Protocol;

namespace OneBotSharp.Plugin;

//插件示例

internal class Program
{
    private static ISendClient send;
    static void Main(string[] args)
    {
        //正向Http
        //send = Bot.MakeSendPipe("http://localhost:8080");
        //反向Http
        //var recv = Bot.MakeRecvPipe("http://0.0.0.0:8081");
        //recv.EventRecv += Recv_EventRecv;

        var pipe = Bot.MakePipe("ws://localhost:8082/", Bot.ConnectType.WebSocket);
        pipe.EventRecv += Recv_EventRecv;
        send = pipe;

        Console.ReadKey();
    }

    private static void Recv_EventRecv(EventBase obj)
    {
        if (obj is EventGroupMessage groupMessage)
        {
            send.SendGroupMsg(SendGroupMsg.Build(groupMessage.GroupId, "你发送了消息：" + groupMessage.Messages[0].BuildSendCq()));

            //groupMessage.BuildReply("你发送了消息：" + groupMessage.Messages[0].BuildSendCq());
        }
    }
}
