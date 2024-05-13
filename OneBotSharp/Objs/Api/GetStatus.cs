using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBotSharp.Objs.Api;

public record GetStatus
{

}

public record GetStatusRes
{
    /// <summary>
    /// 当前 QQ 在线，null 表示无法查询到在线状态
    /// </summary>
    public bool? Online { get; set; }
    /// <summary>
    /// 状态符合预期，意味着各模块正常运行、功能正常，且 QQ 在线
    /// </summary>
    public bool Good { get; set; }
}
