using OneBotSharp.Objs.Api;

namespace OneBotSharp.Http;

public class OneBotHttpClient : IOneBotClient, ISendClient
{
    private readonly HttpClient client;

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

    public Task<bool?> CanSendImage()
    {
        throw new NotImplementedException();
    }

    public Task<bool?> CanSendRecord()
    {
        throw new NotImplementedException();
    }

    public Task CleanCache()
    {
        throw new NotImplementedException();
    }

    public Task<DeleteMsgRes?> DeleteMsg(DeleteMsg msg)
    {
        throw new NotImplementedException();
    }

    public Task<GetCookiesRes?> GetCookies(GetCookies msg)
    {
        throw new NotImplementedException();
    }

    public Task<GetCredentialsRes?> GetCredentials(GetCredentials msg)
    {
        throw new NotImplementedException();
    }

    public Task<GetCsrfTokenRes?> GetCsrfToken()
    {
        throw new NotImplementedException();
    }

    public Task<GetForwardMsgRes?> GetForwardMsg(GetForwardMsg msg)
    {
        throw new NotImplementedException();
    }

    public Task<List<GetFriendListRes>?> GetFriendList()
    {
        throw new NotImplementedException();
    }

    public Task<GetGroupHonorInfoRes?> GetGroupHonorInfo(GetGroupHonorInfo msg)
    {
        throw new NotImplementedException();
    }

    public Task<GetGroupInfoRes?> GetGroupInfo(GetGroupInfo msg)
    {
        throw new NotImplementedException();
    }

    public Task<List<GetGroupListRes>?> GetGroupList()
    {
        throw new NotImplementedException();
    }

    public Task<GetGroupMemberInfoRes?> GetGroupMemberInfo(GetGroupMemberInfo msg)
    {
        throw new NotImplementedException();
    }

    public Task<GetGroupMemberListRes?> GetGroupMemberList(GetGroupMemberList msg)
    {
        throw new NotImplementedException();
    }

    public Task<GetImageRes?> GetImage(GetImage msg)
    {
        throw new NotImplementedException();
    }

    public Task<GetLoginInfoRes?> GetLoginInfo()
    {
        throw new NotImplementedException();
    }

    public Task<GetMsgRes?> GetMsg(GetMsg msg)
    {
        throw new NotImplementedException();
    }

    public Task<GetRecordRes?> GetRecord(GetRecord msg)
    {
        throw new NotImplementedException();
    }

    public Task<GetStatusRes?> GetStatus()
    {
        throw new NotImplementedException();
    }

    public Task<GetStrangerInfoRes?> GetStrangerInfo(GetStrangerInfo msg)
    {
        throw new NotImplementedException();
    }

    public Task<GetVersionInfoRes?> GetVersionInfo()
    {
        throw new NotImplementedException();
    }

    public Task<SendGroupMsgRes?> SendGroupMsg(SendGroupMsg msg)
    {
        throw new NotImplementedException();
    }

    public Task<SendLike?> SendLike(SendLike msg)
    {
        throw new NotImplementedException();
    }

    public Task<SendMsgRes?> SendMsg(SendMsg msg)
    {
        throw new NotImplementedException();
    }

    public Task<SendPrivateMsgRes?> SendPrivateMsg(SendPrivateMsg msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetFriendAddRequestRes?> SetFriendAddRequest(SetFriendAddRequest msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetGroupAddRequestRes?> SetGroupAddRequest(SetGroupAddRequest msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetGroupAdminRes?> SetGroupAdmin(SetGroupAdmin msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetGroupAnonymousRes?> SetGroupAnonymous(SetGroupAnonymous msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetGroupAnonymousBanRes?> SetGroupAnonymousBan(SetGroupAnonymousBan msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetGroupBanRes?> SetGroupBan(SetGroupBan msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetGroupCardRes?> SetGroupCard(SetGroupCard msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetGroupKickRes?> SetGroupKick(SetGroupKick msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetGroupLeaveRes?> SetGroupLeave(SetGroupLeave msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetGroupNameRes?> SetGroupName(SetGroupName msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetGroupSpecialTitleRes?> SetGroupSpecialTitle(SetGroupSpecialTitle msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetGroupWholeBanRes?> SetGroupWholeBan(SetGroupWholeBan msg)
    {
        throw new NotImplementedException();
    }

    public Task<SetRestartRes?> SetRestart(SetRestart msg)
    {
        throw new NotImplementedException();
    }
}
