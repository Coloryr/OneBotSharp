namespace OneBotSharp.Protocol;

public abstract class IOneBotClient : IDisposable
{
    public string? Key { get; init; }
    public required string Url { get; init; }
    public TimeSpan? Timeout { get; init; }

    public abstract void Dispose();
}
