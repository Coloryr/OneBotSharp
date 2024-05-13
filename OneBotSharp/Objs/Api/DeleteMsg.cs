using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneBotSharp.Objs.Api;

public record DeleteMsg
{
    /// <summary>
    /// 消息 ID
    /// </summary>
    [JsonProperty("message_id")]
    public required int MessageId { get; set; }
}

public record DeleteMsgRes
{ 
    
}