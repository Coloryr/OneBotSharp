using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetCredentials
{
    /// <summary>
    /// 需要获取 cookies 的域名
    /// </summary>
    [JsonProperty("domain")]
    public string Domain { get; set; }
}

public record GetCredentialsRes
{
    /// <summary>
    /// Cookies
    /// </summary>
    [JsonProperty("cookies")]
    public string Cookies { get; set; }
    /// <summary>
    /// CSRF Token
    /// </summary>
    [JsonProperty("csrf_token")]
    public int Token { get; set; }
}
