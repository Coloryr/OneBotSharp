﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 戳一戳
/// </summary>
public record MsgPoke : MsgBase
{
    public override string Type => Enums.MsgType.Poke;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// 类型
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }
        /// <summary>
        /// 表情名
        /// </summary>
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

    public override string ToString()
    {
        return $"[戳一戳]";
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

    public static MsgPoke RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Poke)
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

    public static MsgPoke SendParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Poke)
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
