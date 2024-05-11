using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneBotSharp.Objs.Message;

public record MsgText : MsgBase
{
    public const string MsgType = "text";

    public override string Type => MsgType;

    [JsonProperty("data")]
    public MsgData Data { get; set; }
    public record MsgData
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public override string BuildSendCq()
    {
        return CqHelper.Escape(Data.Text);
    }

    public override string BuildRecvCq()
    {
        return CqHelper.UnEscape(Data.Text);
    }

    public static MsgText? JsonParse(JObject text, bool send)
    {
        return text.ToObject<MsgText>(); 
    }

    public static MsgText Build(string text)
    {
        return new()
        {
            Data = new()
            { 
                Text = text
            }
        };
    }
}
