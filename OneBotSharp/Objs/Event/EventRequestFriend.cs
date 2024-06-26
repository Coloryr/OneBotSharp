﻿using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Event;

public record EventRequestFriend : EventRequest
{
    public override string RequestType => Enums.RequestType.Friend;

    public static new EventRequestFriend? JsonParse(JObject obj)
    {
        var msg = obj.ToObject<EventRequestFriend>();
        if (msg == null)
        {
            return null;
        }

        return msg;
    }
}
