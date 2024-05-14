using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
