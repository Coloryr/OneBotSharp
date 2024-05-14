# OneBotSharp

用于`OneBot 11`标准机器人开发插件的C# SDK

目前支持
- 正向Http
- 反向Http
- 正向WebSocket

目前不提供nuget包，你需要将源码拉到你的项目中编译使用  
使用`git submodule`可以方便管理

使用示例
```C#
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
        //创建双向Pipe
        var pipe = Bot.MakePipe("ws://localhost:8082/", Bot.ConnectType.WebSocket);
        pipe.EventRecv += Recv_EventRecv;
        pipe.StateChange += Pipe_StateChange;
        send = pipe;

        Console.ReadKey();
    }

    private static void Pipe_StateChange(ISendRecvPipe.PipeState obj)
    {
        
    }

    /// <summary>
    /// 收到事件，类型可以从Objs.Event里面查看，支持所有事件类型
    /// </summary>
    /// <param name="obj"></param>
    private static void Recv_EventRecv(EventBase obj)
    {
        //收到群消息事件
        if (obj is EventGroupMessage groupMessage)
        {
            send.SendGroupMsg(SendGroupMsg.Build(groupMessage.GroupId, "你发送了消息：" + groupMessage.Messages[0].BuildSendCq()));

            //groupMessage.BuildReply("你发送了消息：" + groupMessage.Messages[0].BuildSendCq());
        }
    }
}
```

Pipe有Send和Recv两种   
Send只能发送数据，及只能调用API  
Recv只能接收数据，及只能收到上报的Event数据  
也有Send和Revc的复合端口