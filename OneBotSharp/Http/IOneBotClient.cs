using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBotSharp.Http;

public abstract class IOneBotClient
{
    public string? Key { get; init; }
    public required string Url { get; init; }
    public TimeSpan? Timeout { get; init; }
}
