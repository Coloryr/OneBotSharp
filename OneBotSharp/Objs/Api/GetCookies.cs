using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetCookies
{
    /// <summary>
    /// 需要获取 cookies 的域名
    /// </summary>
    [JsonProperty("domain")]
    public string Domain { get; set; }
}

public record GetCookiesRes
{
    /// <summary>
    /// Cookies
    /// </summary>
    [JsonProperty("cookies")]
    public string Cookies { get; set; }
}
