using OneBotSharp.Protocol;

namespace OneBotSharp;

public static class Bot
{
    /// <summary>
    /// 创建一个Http发送管道
    /// </summary>
    /// <param name="url">地址</param>
    /// <param name="key">密钥</param>
    /// <returns></returns>
    public static IOneBot<ISendClient> MakeSendPipe(string url, string? key = null)
    {
        return new OneBotHttpClient(url, key);
    }

    /// <summary>
    /// 创建一个Http接收管道
    /// </summary>
    /// <param name="url">地址</param>
    /// <param name="key">密钥</param>
    /// <returns></returns>
    public static IOneBot<IRecvServer> MakeRecvPipe(string url, string? key = null)
    {
        return new OneBotHttpServer(url, key);
    }

    /// <summary>
    /// 创建一个WebSocket双向管道
    /// </summary>
    /// <param name="url">地址</param>
    /// <param name="type">类型</param>
    /// <param name="key">密钥</param>
    /// <returns></returns>
    public static IOneBot<ISendRecvPipe> MakePipe(string url, string? key = null)
    {
        return new OneBotWebSocketClient(url, key);
    }

    /// <summary>
    /// 创建一个WebSocket双向管道
    /// </summary>
    /// <param name="url">地址</param>
    /// <param name="type">类型</param>
    /// <param name="key">密钥</param>
    /// <returns></returns>
    public static IOneBot<ISendRecvPipeServer> MakeServerPipe(string url, string? key = null)
    {
        return new OneBotWebSocketServer(url, key);
    }
}
