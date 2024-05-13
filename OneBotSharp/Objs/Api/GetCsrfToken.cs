using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record GetCsrfToken
{
}

public record GetCsrfTokenRes
{
    /// <summary>
    /// CSRF Token
    /// </summary>
    [JsonProperty("token")]
    public int Token { get; set; }
}