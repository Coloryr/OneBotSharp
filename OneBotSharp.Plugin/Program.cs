using OneBotSharp.Objs.Event;

namespace OneBotSharp.Plugin;

//插件示例

internal class Program
{
    static void Main(string[] args)
    {
        //正向Http
        var send = Bot.MakeSendPipe("http://localhost:8080");
        //反向Http
        var recv = Bot.MakeRecvPipe("http://localhost:8081");
        recv.EventRecv += Recv_EventRecv;

        var pipe = Bot.MakePipe("ws://localhost:8082", Bot.ConnectType.WebSocket);
        pipe.EventRecv += Recv_EventRecv;
    }

    private static void Recv_EventRecv(EventBase obj)
    {
        if (obj is EventGroupMessage groupMessage)
        { 
            
        }
    }
}
