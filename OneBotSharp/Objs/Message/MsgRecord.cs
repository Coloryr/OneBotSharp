using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

public record MsgRecord : MsgBase
{
    [JsonIgnore]
    public const string MsgType = "record";

    public override string Type => MsgType;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        [JsonProperty("file")]
        public string? File { get; set; }
        [JsonProperty("magic")]
        public string? Magic { get; set; }
        [JsonProperty("url")]
        public string? Url { get; set; }
        [JsonProperty("cache")]
        public string? Cache { get; set; }
        [JsonProperty("proxy")]
        public string? Proxy { get; set; }
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
        if (!string.IsNullOrWhiteSpace(Data.Magic))
        {
            code.Datas.Add("magic", Data.Magic.ToString());
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
        if (!string.IsNullOrWhiteSpace(Data.Magic))
        {
            code.Datas.Add("magic", Data.Magic.ToString());
        }
        if (!string.IsNullOrWhiteSpace(Data.Url))
        {
            code.Datas.Add("url", Data.Url);
        }
        return code.ToString();
    }

    public static MsgRecord BuildSendFile(string file, bool magic = false)
    {
        return new()
        {
            Data = new()
            {
                File = "file:///" + Path.GetFullPath(file),
                Magic = magic ? "1" : "0",
                Cache = "1",
                Proxy = "1"
            }
        };
    }

    public static MsgRecord BuildSendUrl(string url, bool magic = false)
    {
        return new()
        {
            Data = new()
            {
                File = url,
                Magic = magic ? "1" : "0",
                Cache = "1",
                Proxy = "1"
            }
        };
    }

    public static MsgRecord BuildRecvUrl(string file, string url, bool magic = false)
    {
        return new()
        {
            Data = new()
            {
                File = file,
                Url = url,
                Magic = magic ? "1" : "0"
            }
        };
    }

    public static MsgRecord BuildBase64(string data, bool magic = false)
    {
        return new()
        {
            Data = new()
            {
                File = "base64://" + data,
                Magic = magic ? "1" : "0",
                Cache = "1",
                Proxy = "1"
            }
        };
    }

    public static MsgRecord BuildBase64(byte[] data, bool magic = false)
    {
        return new()
        {
            Data = new()
            {
                File = "base64://" + Convert.ToBase64String(data),
                Magic = magic ? "1" : "0",
                Cache = "1",
                Proxy = "1"
            }
        };
    }

    public static MsgRecord BuildBase64(Stream data, bool magic = false)
    {
        using var mem = new MemoryStream();
        data.CopyTo(mem);
        return new()
        {
            Data = new()
            {
                File = "base64://" + Convert.ToBase64String(mem.ToArray()),
                Magic = magic ? "1" : "0"
            }
        };
    }

    public static MsgRecord MsgRecvParse(CqCode code)
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
                Magic = code["magic"],
                Url = code["url"]
            }
        };
    }

    public static MsgRecord MsgSendParse(CqCode code)
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
                Magic = code["magic"],
                Cache = code["cache"],
                Proxy = code["proxy"],
                Timeout = code["timeout"]
            }
        };
    }

    public static MsgRecord? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgRecord>();
    }
}
