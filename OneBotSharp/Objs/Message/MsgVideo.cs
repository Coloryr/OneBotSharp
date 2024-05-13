using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

public record MsgVideo : MsgBase
{
    public override string Type => Enums.MsgType.Video;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        [JsonProperty("file")]
        public string? File { get; set; }
        [JsonProperty("url")]
        public string? Url { get; set; }
        [JsonProperty("cache")]
        public string? Cache { get; set; } = "1";
        [JsonProperty("proxy")]
        public string? Proxy { get; set; } = "1";
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
        if (!string.IsNullOrWhiteSpace(Data.Cache))
        {
            code.Datas.Add("cache", Data.Cache.ToString());
        }
        if (!string.IsNullOrWhiteSpace(Data.Proxy))
        {
            code.Datas.Add("proxy", Data.Proxy.ToString());
        }
        if (!string.IsNullOrWhiteSpace(Data.Timeout))
        {
            code.Datas.Add("timeout", Data.Timeout.ToString());
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
        if (!string.IsNullOrWhiteSpace(Data.Url))
        {
            code.Datas.Add("url", Data.Url);
        }
        return code.ToString();
    }

    public static MsgVideo BuildFile(string file)
    {
        return new()
        {
            Data = new()
            {
                File = "file:///" + Path.GetFullPath(file)
            }
        };
    }

    public static MsgVideo BuildUrl(string url)
    {
        return new()
        {
            Data = new()
            {
                File = url
            }
        };
    }

    public static MsgVideo BuildBase64(string data)
    {
        return new()
        {
            Data = new()
            {
                File = "base64://" + data
            }
        };
    }

    public static MsgVideo BuildBase64(byte[] data)
    {
        return new()
        {
            Data = new()
            {
                File = "base64://" + Convert.ToBase64String(data)
            }
        };
    }

    public static MsgVideo BuildBase64(Stream data)
    {
        using var mem = new MemoryStream();
        data.CopyTo(mem);
        return new()
        {
            Data = new()
            {
                File = "base64://" + Convert.ToBase64String(mem.ToArray())
            }
        };
    }

    public static MsgVideo RecvParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Video)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                File = code["file"],
                Url = code["url"]
            }
        };
    }

    public static MsgVideo SendParse(CqCode code)
    {
        if (code.Type != Enums.MsgType.Video)
        {
            throw new ArgumentException("cqcode type error");
        }

        return new()
        {
            Data = new()
            {
                File = code["file"],
                Cache = code["cache"],
                Proxy = code["proxy"],
                Timeout = code["timeout"]
            }
        };
    }

    public static MsgVideo? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgVideo>();
    }
}
