using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 音乐分享
/// </summary>
public record MsgMusic : MsgBase
{
    public override string Type => Enums.MsgType.Music;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// 分享的类型
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }
        /// <summary>
        /// 歌曲 ID
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// 点击后跳转目标 URL
        /// </summary>
        [JsonProperty("url")]
        public string? Url { get; set; }
        /// <summary>
        /// 音乐 URL
        /// </summary>
        [JsonProperty("audio")]
        public string? Audio { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        public string? Title { get; set; }
        /// <summary>
        /// 发送时可选，内容描述
        /// </summary>
        [JsonProperty("content")]
        public string? Content { get; set; }
        /// <summary>
        /// 发送时可选，图片 URL
        /// </summary>
        [JsonProperty("image")]
        public string? Image { get; set; }
    }

    public override string BuildSendCq()
    {
        if (Data.Type == Enums.MusicType.Custom)
        {
            var code = new CqCode { Type = Type };
            if (!string.IsNullOrWhiteSpace(Data.Url))
            {
                code.Datas.Add("url", Data.Url);
            }
            if (!string.IsNullOrWhiteSpace(Data.Audio))
            {
                code.Datas.Add("audio", Data.Audio);
            }
            if (!string.IsNullOrWhiteSpace(Data.Title))
            {
                code.Datas.Add("title", Data.Title);
            }
            if (!string.IsNullOrWhiteSpace(Data.Content))
            {
                code.Datas.Add("content", Data.Content);
            }
            if (!string.IsNullOrWhiteSpace(Data.Image))
            {
                code.Datas.Add("image", Data.Image);
            }

            return code.ToString();
        }
        return $"[CQ:music,type={Data.Type},id={Data.Id}]";
    }

    public override string BuildRecvCq()
    {
        return BuildSendCq();
    }

    public override string ToString()
    {
        var str = $"音乐分享：";
        if (Data.Type == Enums.MusicType.QQ)
        {
            str += "QQ音乐";
        }
        else if (Data.Type == Enums.MusicType.N163)
        {
            str += "网易云音乐";
        }
        else if (Data.Type == Enums.MusicType.XM)
        {
            str += "虾米音乐";
        }
        else
        {
            str += "其他音乐";
        }

        if (Data.Title is { } title)
        {
            str += "," + title;
        }
        if (Data.Content is { } content)
        {
            str += "," + content;
        }

        return str;
    }

    public static MsgMusic BuildQQ(string id)
    {
        return new()
        {
            Data = new()
            {
                Type = Enums.MusicType.QQ,
                Id = id
            }
        };
    }

    public static MsgMusic Build163(string id)
    {
        return new()
        {
            Data = new()
            {
                Type = Enums.MusicType.N163,
                Id = id
            }
        };
    }

    public static MsgMusic BuildXM(string id)
    {
        return new()
        {
            Data = new()
            {
                Type = Enums.MusicType.XM,
                Id = id
            }
        };
    }

    public static MsgMusic BuildCustom(string url, string audio, string title, string? content = null, string? image = null)
    {
        return new()
        {
            Data = new()
            {
                Type = Enums.MusicType.Custom,
                Url = url,
                Audio = audio,
                Title = title,
                Content = content,
                Image = image
            }
        };
    }

    public static MsgMusic RecvParse(CqCode code)
    {
        throw new Exception("can not decode recv msg music");
    }

    public static MsgMusic SendParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Music)
        {
            throw new ArgumentException("cqcode type error");
        }

        var type = code["type"];
        if (type == Enums.MusicType.Custom)
        {
            return new()
            {
                Data = new()
                {
                    Type = type,
                    Title = code["title"],
                    Audio = code["audio"],
                    Url = code["url"],
                    Image = code["image"],
                    Content = code["content"]
                }
            };
        }

        return new()
        {
            Data = new()
            {
                Type = type,
                Id = code["id"]
            }
        };
    }

    public static MsgMusic? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgMusic>();
    }
}
