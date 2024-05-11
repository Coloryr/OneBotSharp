namespace OneBotSharp.Http;

public class OneBotHttpClient
{
    public string? Key { get; init; }
    public required string Url { get; init; }
    public TimeSpan? Timeout { get; init; }

    private HttpClient client;

    public OneBotHttpClient()
    {
        if (Url == null)
        {
            throw new ArgumentNullException(nameof(Url), "Url is null");
        }
        client = new()
        {
            BaseAddress = new Uri(Url)
        };
        if (Timeout is { } time)
        {
            client.Timeout = time;
        }
        if (Key is { })
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Key}");
        }
    }


}
