using Newtonsoft.Json.Linq;
using OneBotSharp.Objs.Message;

namespace OneBotSharp.Test;

internal class Program
{
    static void Main(string[] args)
    {
        //CqTest();
        //MsgTest();
        //MsgCqTest();
        MsgJsonTest();
    }

    private static void CqTest()
    {
        var cq = CqCode.Parse("[CQ:image,file=123.jpg]");
        var data = cq.ToString();

        cq = CqCode.Parse("[CQ:share,title=标题中有=等号,url=http://baidu.com]");
        data = cq.ToString();

        cq = CqCode.Parse("[CQ:share,title=震惊&#44;小伙睡觉前居然...,url=http://baidu.com/?a=1&amp;b=2]");
        data = cq.ToString();
    }

    private static void MsgTest()
    {
        var cq = "[CQ:image,url=http://123.123.123]";
        var code = CqCode.Parse(cq);
        var msg = MsgBase.ParseRecv(code);

        var text = msg.BuildJson();
        var text1 = msg.BuildRecvCq();

        var text2 = "[CQ:node,user_id=10001000,nickname=某人,content=&#91;CQ:face&#44;id=123&#93;哈喽～]";
        var code1 = CqCode.Parse(text2);
        var msg1 = MsgBase.ParseRecv(code1);

        var text3 = msg1.BuildJson();
        var text4 = msg1.BuildRecvCq();
    }

    private static void MsgCqTest()
    {
        var text = "Cesshi[CQ:face,id=123]哈喽～[CQ:image,file=123.jpg]123412313432[CQ:dice]&#91;CQ:face&#44;id=123&#93;哈喽～";
        var list = CqHelper.ParseMsg(text);

        var text1 = "[CQ:node,user_id=10001000,nickname=某人,content=&#91;CQ:face&#44;id=123&#93;哈喽～]";
        var list1 = CqHelper.ParseMsg(text1);
    }

    private static void MsgJsonTest()
    {
        var json = 
"""
{
    "type": "node",
    "data": {
        "user_id": "10001000",
        "nickname": "某人",
        "content": "[CQ:face,id=123]哈喽～"
    }
}
""";
        var node = MsgBase.ParseRecv(JObject.Parse(json));

        var json1 =
"""
{
    "type": "node",
    "data": {
        "user_id": "10001000",
        "nickname": "某人",
        "content": [
            {"type": "face", "data": {"id": "123"}},
            {"type": "text", "data": {"text": "哈喽～"}}
        ]
    }
}
""";
        var node1 = MsgBase.ParseRecv(JObject.Parse(json1));
    }
}
