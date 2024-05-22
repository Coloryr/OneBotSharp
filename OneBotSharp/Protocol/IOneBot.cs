namespace OneBotSharp.Protocol;

public abstract class IOneBot<T>(string url, string? key) : IDisposable
{
    public abstract T Pipe { get; }
    public string? Key { get; init; } = key;
    public string Url { get; init; } = url;
    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(10);

    public abstract void Dispose();

    public abstract Task Start();
    public abstract Task Close();
}
