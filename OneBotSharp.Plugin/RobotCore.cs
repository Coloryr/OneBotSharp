using System.Collections.Concurrent;
using OneBotSharp.Objs.Api;
using OneBotSharp.Objs.Event;
using OneBotSharp.Objs.Message;
using OneBotSharp.Protocol;

namespace OneBotSharp.Plugin;

public static class RobotCore
{
    public static IOneBot<ISendRecvPipe> Robot;
    private static Thread _thread;
    private static bool _run;
    private static bool _send;
    private static bool _restart;
    private static ConcurrentQueue<Action> _list = [];

    public static void Start(string url, string key)
    {
        Robot = Bot.MakePipe(url, key);
        Robot.Pipe.EventRecv += Robot_EventRecv;
        Robot.Pipe.StateChange += Pipe_StateChange;
        _thread = new Thread(Run);
        _run = true;
        _thread.Start();
        Connect();
    }

    private static void Run()
    {
        while (_run)
        {
            if (_send)
            {
                while (_list.TryDequeue(out var runa))
                {
                    runa();
                }
                Thread.Sleep(20);
            }
            else if (_restart)
            {
                _restart = false;
                Connect();
                Thread.Sleep(5000);
            }
        }
    }

    private static async void Pipe_StateChange(ISendRecvPipe arg1, ISendRecvPipe.PipeState arg2)
    {
        if (arg2 is ISendRecvPipe.PipeState.ConnectFail
            or ISendRecvPipe.PipeState.Disconnected)
        {
            _send = false;
            if (_run == false)
            {
                return;
            }
            await Robot.Close();
            _restart = true;
        }
        else if (arg2 == ISendRecvPipe.PipeState.Connected)
        {
            _send = true;
        }
    }

    private static void Connect()
    {
        try
        {
            Robot.Start();
        }
        catch (Exception e)
        {
            if (_run == false)
            {
                return;
            }
            Console.WriteLine(e);
            _restart = true;
        }
    }

    private static void Robot_EventRecv(ISendRecvPipe pipe, EventBase obj)
    {
        if (obj is EventGroupMessage message)
        {
            Console.WriteLine(message);
        }
    }

    public static void Stop()
    {
        _run = false;
        Robot.Close();
        Robot.Dispose();
    }

    public static void SendPrivateMessage(long sendQQ, List<MsgBase> value)
    {
        _list.Enqueue(() =>
        {
            Robot.Pipe.SendPrivateMsg(SendPrivateMsg.Build(sendQQ, value));
        });
    }

    public static void SendGroupMessage(long sendQQ, List<MsgBase> value)
    {
        _list.Enqueue(() =>
        {
            Robot.Pipe.SendGroupMsg(SendGroupMsg.Build(sendQQ, value));
        });
    }
}
