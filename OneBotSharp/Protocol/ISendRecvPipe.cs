namespace OneBotSharp.Protocol;

public interface ISendRecvPipe : ISendClient, IRecvServer
{
    public enum PipeState
    {
        Connected, ConnectFail, Disconnected
    }
    public event Action<PipeState>? StateChange;

    public void Ping();
}
