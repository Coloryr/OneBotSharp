using System.Text;

namespace OneBotSharp;

public class CqCode
{
    public readonly Dictionary<string, string> Datas = [];
    public required string Type { get; init; }

    /// <summary>
    /// 直接获取值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string? this[string key] => TryGet(key);

    /// <summary>
    /// 尝试获取值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private string? TryGet(string key)
    {
        if (Datas.TryGetValue(key, out var value))
        {
            return value;
        }

        return null;
    }

    /// <summary>
    /// 构建CQ码消息
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (string.IsNullOrWhiteSpace(Type))
        {
            throw new Exception("Cq Type is empty");
        }
        var builder = new StringBuilder();
        builder.Append("[CQ:").Append(CqHelper.Escape(Type));
        foreach (var item in Datas)
        {
            builder.Append(',').Append($"{CqHelper.Escape(item.Key)}={CqHelper.Escape(item.Value)}");
        }
        builder.Append(']');

        return builder.ToString();
    }

    /// <summary>
    /// 解析一整个CQ码
    /// </summary>
    /// <param name="text">CQ码数据</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static CqCode Parse(string text)
    {
        if (!text.StartsWith("[CQ:") || !text.EndsWith(']'))
        {
            throw new ArgumentException("text is not CQ code");
        }

        text = text[4..^1];
        var arg = text.Split(',');
        var code = new CqCode()
        {
            Type = arg[0]
        };
        for (int a = 1; a < arg.Length; a++)
        {
            var item = arg[a];
            var data = CqHelper.UnEscape(item);
            int index = data.IndexOf('=');
            if (index < 1)
            {
                code.Datas.Add(data, "");
                continue;
            }
            var key = data[..index];
            var value = data[(index + 1)..];
            code.Datas.Add(key, value);
        }

        return code;
    }
}
