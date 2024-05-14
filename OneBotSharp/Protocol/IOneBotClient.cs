using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBotSharp.Protocol;

public abstract class IOneBotClient : IDisposable
{
    public string? Key { get; init; }
    public required string Url { get; init; }
    public TimeSpan? Timeout { get; init; }

    public abstract void Dispose();
}
