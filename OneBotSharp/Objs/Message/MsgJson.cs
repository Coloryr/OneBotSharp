﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// JSON 消息
/// </summary>
public record MsgJson : MsgBase
{
    public override string Type => Enums.MsgType.Json;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// JSON 内容
        /// </summary>
        [JsonProperty("data")]
        public string? Data { get; set; }
    }

    public override string BuildSendCq()
    {
        return $"[CQ:json,data={Data.Data}]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public override string ToString()
    {
        return $"[Json消息]";
    }

    public static MsgJson Build(string json)
    {
        return new()
        {
            Data = new()
            {
                Data = json
            }
        };
    }

    public static MsgJson Build(JObject obj)
    {
        return new()
        {
            Data = new()
            {
                Data = obj.ToString()
            }
        };
    }

    public static MsgJson RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Json)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                Data = code["json"]
            }
        };
    }

    public static MsgJson SendParse(CqCode code)
    {
        return RecvParse(code);
    }

    public static MsgJson? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgJson>();
    }
}
