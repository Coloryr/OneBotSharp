using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneBotSharp.Objs.Event;

namespace OneBotSharp.Protocol;

public interface IRecvServer
{
    public event Action<EventBase>? EventRecv;
}
