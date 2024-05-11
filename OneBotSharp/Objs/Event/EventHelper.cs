using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneBotSharp.Objs.Message;

namespace OneBotSharp.Objs.Event;

public static class EventHelper
{
    public static EventReply.Private BuildReply(this EventPrivateMessage _, string message, bool cqcpde = false)
    {
        return new()
        {
            Message = message,
            Escape = cqcpde
        };
    }

    public static EventReply.Private BuildReply(this EventPrivateMessage _, List<MsgBase> message)
    {
        return new()
        {
            Message = message,
            Escape = false
        };
    }
}
