namespace OneBotSharp.Protocol;

public abstract class IOneBotClient(string url, string? key) : IDisposable
{
    public string? Key { get; init; } = key;
    public string Url { get; init; } = url;
    public TimeSpan? Timeout { get; init; }

    public abstract void Dispose();
}
