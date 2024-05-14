using OneBotSharp.Objs.Event;

namespace OneBotSharp.Protocol;

public interface IRecvServer
{
    public event Action<EventBase>? EventRecv;
}
