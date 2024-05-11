using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

/// <summary>
/// 图片
/// </summary>
public record MsgImage : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "image";
    [JsonIgnore]
    public const string Flash = "flash";

    public override string Type => MsgType;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        /// <summary>
        /// 图片文件名
        /// </summary>
        [JsonProperty("file")]
        public string? File { get; set; }
        /// <summary>
        /// 图片类型，flash 表示闪照
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }
        /// <summary>
        /// 图片 URL
        /// </summary>
        [JsonProperty("url")]
        public string? Url { get; set; }
        /// <summary>
        /// 是否使用已缓存的文件
        /// </summary>
        [JsonProperty("cache")]
        public string? Cache { get; set; }
        /// <summary>
        /// 是否通过代理下载文件
        /// </summary>
        [JsonProperty("proxy")]
        public string? Proxy { get; set; }
        /// <summary>
        /// 下载网络文件的超时时间
        /// </summary>
        [JsonProperty("timeout")]
        public string? Timeout { get; set; }
    }

    public override string BuildSendCq()
    {
        var code = new CqCode { Type = Type };
        if (!string.IsNullOrWhiteSpace(Data.File))
        {
            code.Datas.Add("file", Data.File);
        }
        if (!string.IsNullOrWhiteSpace(Data.Type))
        {
            code.Datas.Add("type", Data.Type);
        }
        if (!string.IsNullOrWhiteSpace(Data.Cache))
        {
            code.Datas.Add("cache", Data.Cache);
        }
        if (!string.IsNullOrWhiteSpace(Data.Proxy))
        {
            code.Datas.Add("proxy", Data.Proxy);
        }
        if (!string.IsNullOrWhiteSpace(Data.Timeout))
        {
            code.Datas.Add("timeout", Data.Timeout);
        }

        return code.ToString();
    }

    public override string BuildRecvCq()
    {
        var code = new CqCode { Type = Type };
        if (!string.IsNullOrWhiteSpace(Data.File))
        {
            code.Datas.Add("file", Data.File);
        }
        if (!string.IsNullOrWhiteSpace(Data.Type))
        {
            code.Datas.Add("type", Data.Type);
        }
        if (!string.IsNullOrWhiteSpace(Data.Url))
        {
            code.Datas.Add("url", Data.Url);
        }

        return code.ToString();
    }

    public static MsgImage BuildSendFile(string file, bool flash = false)
    {
        return new()
        {
            Data = new()
            {
                File = "file:///" + Path.GetFullPath(file),
                Type = flash ? Flash : null,
                Cache = "1",
                Proxy = "1"
            }
        };
    }

    public static MsgImage BuildSendUrl(string url, bool flash = false)
    {
        return new()
        {
            Data = new()
            {
                File = url,
                Type = flash ? Flash : null,
                Cache = "1",
                Proxy = "1"
            }
        };
    }

    public static MsgImage BuildSendBase64(string data, bool flash = false)
    {
        return new()
        {
            Data = new()
            {
                File = "base64://" + data,
                Type = flash ? Flash : null,
                Cache = "1",
                Proxy = "1"
            }
        };
    }

    public static MsgImage BuildSendBase64(byte[] data, bool flash = false)
    {
        return new()
        {
            Data = new()
            {
                File = "base64://" + Convert.ToBase64String(data),
                Type = flash ? Flash : null,
                Cache = "1",
                Proxy = "1"
            }
        };
    }

    public static MsgImage BuildSendBase64(Stream data, bool flash = false)
    {
        using var mem = new MemoryStream();
        data.CopyTo(mem);
        return new()
        {
            Data = new()
            {
                File = "base64://" + Convert.ToBase64String(mem.ToArray()),
                Type = flash ? Flash : null,
                Cache = "1",
                Proxy = "1"
            }
        };
    }

    public static MsgImage BuildRecvUrl(string file, string url, bool flash)
    {
        return new()
        {
            Data = new()
            {
                File = file,
                Url = url,
                Type = flash ? Flash : null
            }
        };
    }

    public static MsgImage MsgRecvParse(CqCode code)
    {
        if (code.Type != MsgType)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                File = code["file"],
                Type = code["type"],
                Url = code["url"]
            }
        };
    }

    public static MsgImage MsgSendParse(CqCode code)
    {
        if (code.Type != MsgType)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                File = code["file"],
                Type = code["type"],
                Cache = code["cache"],
                Proxy = code["proxy"],
                Timeout = code["timeout"]
            }
        };
    }

    public static MsgImage? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgImage>();
    }
}
