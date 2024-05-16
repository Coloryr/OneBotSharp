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
        //var bot = Bot.MakeSendPipe("http://localhost:8080");
        //send = bot.Pipe;
        //反向Http
        //var recv = Bot.MakeRecvPipe("http://0.0.0.0:8081");
        //recv.EventRecv += Recv_EventRecv;
        //创建双向Pipe
        var bot = Bot.MakePipe("ws://localhost:8082/");
        bot.Pipe.EventRecv += Recv_EventRecv;
        bot.Pipe.StateChange += Pipe_StateChange;
        send = bot.Pipe;

        Console.ReadKey();
    }

    private static void Pipe_StateChange(ISendRecvPipe pipe, ISendRecvPipe.PipeState obj)
    {
        
    }

    /// <summary>
    /// 收到事件，类型可以从Objs.Event里面查看，支持所有事件类型
    /// </summary>
    /// <param name="obj"></param>
    private static void Recv_EventRecv(ISendRecvPipe pipe, EventBase obj)
    {
        //收到群消息事件
        if (obj is EventGroupMessage groupMessage)
        {
            send.SendGroupMsg(SendGroupMsg.Build(groupMessage.GroupId, "你发送了消息：" + groupMessage.Messages[0].BuildSendCq()));

            //groupMessage.BuildReply("你发送了消息：" + groupMessage.Messages[0].BuildSendCq());
        }
    }
}
