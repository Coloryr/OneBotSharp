﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

public record MsgPoke : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "poke";

    public override string Type => MsgType;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        [JsonProperty("type")]
        public string? Type { get; set; }
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
    }

    public override string BuildSendCq()
    {
        return $"[CQ:at,type={Data.Type},id={Data.Id}]";
    }

    public override string BuildRecvCq()
    {
        return $"[CQ:at,type={Data.Type},id={Data.Id},name={Data.Name}]";
    }

    public static MsgPoke Build(string type, string id)
    {
        return new()
        {
            Data = new()
            {
                Type = type,
                Id = id
            }
        };
    }

    public static MsgPoke MsgRecvParse(CqCode code)
    {
        if (code.Type != MsgType)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                Type = code["type"],
                Id = code["id"],
                Name = code["name"]
            }
        };
    }

    public static MsgPoke MsgSendParse(CqCode code)
    {
        if (code.Type != MsgType)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                Type = code["type"],
                Id = code["id"]
            }
        };
    }

    public static MsgPoke? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgPoke>();
    }
}
