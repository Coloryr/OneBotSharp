using System.Text;
using OneBotSharp.Objs.Message;

namespace OneBotSharp;

public static class CqHelper
{
    public static Dictionary<string, string> EscapeList = new()
    {
        { "&", "&amp;" },
        { "[", "&#91;" },
        { "]", "&#93;" },
        { ",", "&#44;" },
    };

    public static string Escape(string text)
    {
        foreach (var item in EscapeList)
        {
            text = text.Replace(item.Key, item.Value);
        }

        return text;
    }

    public static string UnEscape(string text)
    {
        foreach (var item in EscapeList)
        {
            text = text.Replace(item.Value, item.Key);
        }

        return text;
    }

    /// <summary>
    /// 将CQ码消息进行拆解
    /// </summary>
    /// <param name="data">消息字符串</param>
    /// <param name="send">是否是从插件接收到的消息</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static List<MsgBase> ParseMsg(string data, bool send = false)
    {
        var list = new List<MsgBase>();
        bool cq = false;
        for (int now = 0; now < data.Length;)
        {
            var index = data.IndexOf("[CQ:", now);
            if (index > 0)
            {
                cq = true;
                var data1 = data[now..index];
                list.Add(MsgText.Build(UnEscape(data1)));
                now += data1.Length;
                index = 0;
            }
            if (index == 0)
            {
                int index1 = data.IndexOf(']', now);
                if (index1 < 0 && cq)
                {
                    throw new Exception("cqcode missing ']'");
                }

                var cqcode = data[now..(index1 + 1)];
                var code = CqCode.Parse(cqcode);
                var msg = send ? MsgBase.ParseSend(code)
                    : MsgBase.ParseRecv(code);
                if (msg != null)
                {
                    list.Add(msg);
                }
                now += cqcode.Length;
                cq = false;
            }
            if (index < 0)
            {
                var data1 = data[now..];
                list.Add(MsgText.Build(UnEscape(data1)));
                now += data1.Length;
            }
        }

        return list;
    }

    /// <summary>
    /// 构建发送给Onebot的CQ消息
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static string BuildSendMsg(List<MsgBase> list)
    {
        var builder = new StringBuilder();
        foreach (var item in list)
        {
            builder.Append(item.BuildSendCq());
        }

        return builder.ToString();
    }

    /// <summary>
    /// 构建发送给插件的CQ消息
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static string BuildRecvMsg(List<MsgBase> list)
    {
        var builder = new StringBuilder();
        foreach (var item in list)
        {
            builder.Append(item.BuildRecvCq());
        }

        return builder.ToString();
    }
}
