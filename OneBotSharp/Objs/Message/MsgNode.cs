using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 合并转发自定义节点
/// </summary>
public record MsgNode : MsgBase
{
    public override string Type => Enums.MsgType.Node;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// 转发的消息 ID
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }
        /// <summary>
        /// 发送者 QQ 号
        /// </summary>
        [JsonProperty("user_id")]
        public string? UserId { get; set; }
        /// <summary>
        /// 发送者昵称
        /// </summary>
        [JsonProperty("nickname")]
        public string? NickName { get; set; }
        /// <summary>
        /// 不要从这里取，从Data外面的Messages取
        /// </summary>
        [JsonProperty("content")]
        public object? Content { get; set; }
    }

    /// <summary>
    /// 消息内容
    /// </summary>
    [JsonIgnore]
    public List<MsgBase> Messages { get; init; } = [];

    public override string BuildSendCq()
    {
        var code = new CqCode { Type = Type };

        if (string.IsNullOrWhiteSpace(Data.Id))
        {
            if (!string.IsNullOrWhiteSpace(Data.UserId))
            {
                code.Datas.Add("user_id", Data.UserId);
            }
            if (!string.IsNullOrWhiteSpace(Data.NickName))
            {
                code.Datas.Add("nickname", Data.NickName);
            }

            code.Datas.Add("content", CqHelper.BuildSendMsg(Messages));
            return code.ToString();
        }

        return $"[CQ:node,id={Data.Id}]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public override string BuildJson()
    {
        Data.Content = Messages;

        return JsonConvert.SerializeObject(this);
    }

    public static MsgNode Build(string id)
    {
        return new()
        {
            Data = new()
            {
                Id = id
            }
        };
    }

    public static MsgNode Build(string id, string nick, List<MsgBase> msg)
    {
        return new()
        {
            Data = new()
            {
                UserId = id,
                NickName = nick,
            },
            Messages = msg
        };
    }

    public static MsgNode RecvParse(CqCode code)
    {
        return Parse(code, false);
    }

    public static MsgNode SendParse(CqCode code)
    {
        return Parse(code, true);
    }

    private static MsgNode Parse(CqCode code, bool send)
    {
        if (code.Type != Enums.MsgType.Node)
        {
            throw new ArgumentException("cqcode type error");
        }

        var id = code["id"];
        if (id != null)
        {
            return new()
            {
                Data = new()
                {
                    Id = id
                }
            };
        }

        if (code["content"] is not { } content)
        {
            throw new Exception("cqcode content is null");
        }

        return new()
        {
            Data = new()
            {
                NickName = code["nickname"],
                UserId = code["user_id"]
            },
            Messages = CqHelper.ParseMsg(content, send)
        };
    }

    public static MsgNode? JsonParse(JObject text, bool send)
    {
        var node = text.ToObject<MsgNode>();
        if (node == null)
        {
            return null;
        }
        if (node.Data.Content is { } content)
        {
            if (content is string str)
            {
                node.Messages.AddRange(CqHelper.ParseMsg(str, send));
            }
            else if (content is JArray list)
            {
                foreach (var item in list)
                {
                    var item1 = (item as JObject)!;
                    var msg = send ? ParseSend(item1) : ParseRecv(item1);
                    if (msg != null)
                    {
                        node.Messages.Add(msg);
                    }
                }
            }
        }

        return node;
    }
}
