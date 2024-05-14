using OneBotSharp.Protocol;

namespace OneBotSharp;

public static class Bot
{
    public enum ConnectType
    {
        WebSocket, 
        //WebSocketServer
    }

    /// <summary>
    /// 创建一个Http发送管道
    /// </summary>
    /// <param name="url">地址</param>
    /// <param name="key">密钥</param>
    /// <returns></returns>
    public static ISendClient MakeSendPipe(string url, string? key = null)
    {
        return new OneBotHttpClient(url, key);
    }

    /// <summary>
    /// 创建一个Http接收管道
    /// </summary>
    /// <param name="url">地址</param>
    /// <param name="key">密钥</param>
    /// <returns></returns>
    public static IRecvServer MakeRecvPipe(string url, string? key = null)
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
    public static ISendRecvPipe MakePipe(string url, ConnectType type, string? key = null)
    {
        return type switch
        {
            //ConnectType.WebSocketServer => new OneBotWebSocketServer(url, key),
            _ => new OneBotWebSocketClient(url, key)
        };
    }
}
