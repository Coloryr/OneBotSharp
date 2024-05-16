using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneBotSharp.Objs.Api;

namespace OneBotSharp.Protocol;

public class OneBotHttpClient : IOneBot<ISendClient>, ISendClient
{
    private readonly HttpClient client;

    public override ISendClient Pipe => this;

    public OneBotHttpClient(string url, string? key = null) : base(url, key)
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

    private Task<HttpResponseMessage> Post(string url, string data = "")
    {
        var content = new StringContent(data, MediaTypeHeaderValue.Parse("application/json"));

        return client.PostAsync(url, content);
    }

    private async Task<T?> Post<T>(string url, object obj)
    {
        var data = await Post(url, JsonConvert.SerializeObject(obj));
        if (!data.IsSuccessStatusCode)
        {
            return default;
        }

        var text = await data.Content.ReadAsStringAsync();
        var obj1 = JObject.Parse(text);
        if (obj1["status"]?.ToString() == "ok"
            && obj1["data"] is { } data1)
        {
            return data1.ToObject<T>();
        }

        return default;
    }

    private async Task<T?> Post<T>(string url)
    {
        var data = await Post(url, "");
        if (!data.IsSuccessStatusCode)
        {
            return default;
        }
        var text = await data.Content.ReadAsStringAsync();
        var obj = JObject.Parse(text);
        if (obj["status"]?.ToString() == "ok"
            && obj["data"] is { } data1)
        {
            return data1.ToObject<T>();
        }

        return default;
    }

    public async Task<bool?> CanSendImage()
    {
        var data = await Post<JObject>(SendUrl.CanSendImage);
        if (data == null)
        {
            return null;
        }

        return data["yes"]?.Value<bool>();
    }

    public async Task<bool?> CanSendRecord()
    {
        var data = await Post<JObject>(SendUrl.CanSendRecord);
        if (data == null)
        {
            return null;
        }

        return data["yes"]?.Value<bool>();
    }

    public Task CleanCache()
    {
        return Post(SendUrl.CleanCache);
    }

    public Task<DeleteMsgRes?> DeleteMsg(DeleteMsg msg)
    {
        return Post<DeleteMsgRes>(SendUrl.DeleteMsg, msg);
    }

    public Task<GetCookiesRes?> GetCookies(GetCookies msg)
    {
        return Post<GetCookiesRes>(SendUrl.GetCookies, msg);
    }

    public Task<GetCredentialsRes?> GetCredentials(GetCredentials msg)
    {
        return Post<GetCredentialsRes>(SendUrl.GetCredentials, msg);
    }

    public Task<GetCsrfTokenRes?> GetCsrfToken()
    {
        return Post<GetCsrfTokenRes>(SendUrl.GetCsrfToken, "");
    }

    public async Task<GetForwardMsgRes?> GetForwardMsg(GetForwardMsg msg)
    {
        var res = await Post<GetForwardMsgRes>(SendUrl.GetForwardMsg, msg);
        if (res is { })
        {
            res.Parse();
        }

        return res;
    }

    public Task<List<GetFriendListRes>?> GetFriendList()
    {
        return Post<List<GetFriendListRes>>(SendUrl.GetFriendList);
    }

    public Task<GetGroupHonorInfoRes?> GetGroupHonorInfo(GetGroupHonorInfo msg)
    {
        return Post<GetGroupHonorInfoRes>(SendUrl.GetGroupHonorInfo, msg);
    }

    public Task<GetGroupInfoRes?> GetGroupInfo(GetGroupInfo msg)
    {
        return Post<GetGroupInfoRes>(SendUrl.GetGroupInfo, msg);
    }

    public Task<List<GetGroupListRes>?> GetGroupList()
    {
        return Post<List<GetGroupListRes>>(SendUrl.GetGroupList);
    }

    public Task<GetGroupMemberInfoRes?> GetGroupMemberInfo(GetGroupMemberInfo msg)
    {
        return Post<GetGroupMemberInfoRes>(SendUrl.GetGroupMemberInfo, msg);
    }

    public Task<GetGroupMemberListRes?> GetGroupMemberList(GetGroupMemberList msg)
    {
        return Post<GetGroupMemberListRes>(SendUrl.GetGroupMemberList, msg);
    }

    public Task<GetImageRes?> GetImage(GetImage msg)
    {
        return Post<GetImageRes>(SendUrl.GetImage, msg);
    }

    public Task<GetLoginInfoRes?> GetLoginInfo()
    {
        return Post<GetLoginInfoRes>(SendUrl.GetLoginInfo);
    }

    public async Task<GetMsgRes?> GetMsg(GetMsg msg)
    {
        var res = await Post<GetMsgRes>(SendUrl.GetMsg);
        if (res is { })
        {
            res.Parse();
        }
        return res;
    }

    public Task<GetRecordRes?> GetRecord(GetRecord msg)
    {
        return Post<GetRecordRes>(SendUrl.GetRecord, msg);
    }

    public Task<GetStatusRes?> GetStatus()
    {
        return Post<GetStatusRes>(SendUrl.GetStatus);
    }

    public Task<GetStrangerInfoRes?> GetStrangerInfo(GetStrangerInfo msg)
    {
        return Post<GetStrangerInfoRes>(SendUrl.GetStrangerInfo, msg);
    }

    public Task<GetVersionInfoRes?> GetVersionInfo()
    {
        return Post<GetVersionInfoRes>(SendUrl.GetVersionInfo);
    }

    public Task<SendGroupMsgRes?> SendGroupMsg(SendGroupMsg msg)
    {
        return Post<SendGroupMsgRes>(SendUrl.SendGroupMsg, msg);
    }

    public Task<SendLikeRes?> SendLike(SendLike msg)
    {
        return Post<SendLikeRes>(SendUrl.SendLike, msg);
    }

    public Task<SendMsgRes?> SendMsg(SendMsg msg)
    {
        return Post<SendMsgRes>(SendUrl.SendMsg, msg);
    }

    public Task<SendPrivateMsgRes?> SendPrivateMsg(SendPrivateMsg msg)
    {
        return Post<SendPrivateMsgRes>(SendUrl.SendPrivateMsg, msg);
    }

    public Task<SetFriendAddRequestRes?> SetFriendAddRequest(SetFriendAddRequest msg)
    {
        return Post<SetFriendAddRequestRes>(SendUrl.SetFriendAddRequest, msg);
    }

    public Task<SetGroupAddRequestRes?> SetGroupAddRequest(SetGroupAddRequest msg)
    {
        return Post<SetGroupAddRequestRes>(SendUrl.SetGroupAddRequest, msg);
    }

    public Task<SetGroupAdminRes?> SetGroupAdmin(SetGroupAdmin msg)
    {
        return Post<SetGroupAdminRes>(SendUrl.SetGroupAdmin, msg);
    }

    public Task<SetGroupAnonymousRes?> SetGroupAnonymous(SetGroupAnonymous msg)
    {
        return Post<SetGroupAnonymousRes>(SendUrl.SetGroupAnonymous, msg);
    }

    public Task<SetGroupAnonymousBanRes?> SetGroupAnonymousBan(SetGroupAnonymousBan msg)
    {
        return Post<SetGroupAnonymousBanRes>(SendUrl.SetGroupAnonymousBan, msg);
    }

    public Task<SetGroupBanRes?> SetGroupBan(SetGroupBan msg)
    {
        return Post<SetGroupBanRes>(SendUrl.SetGroupBan, msg);
    }

    public Task<SetGroupCardRes?> SetGroupCard(SetGroupCard msg)
    {
        return Post<SetGroupCardRes>(SendUrl.SetGroupCard, msg);
    }

    public Task<SetGroupKickRes?> SetGroupKick(SetGroupKick msg)
    {
        return Post<SetGroupKickRes>(SendUrl.SetGroupKick, msg);
    }

    public Task<SetGroupLeaveRes?> SetGroupLeave(SetGroupLeave msg)
    {
        return Post<SetGroupLeaveRes>(SendUrl.SetGroupLeave, msg);
    }

    public Task<SetGroupNameRes?> SetGroupName(SetGroupName msg)
    {
        return Post<SetGroupNameRes>(SendUrl.SetGroupName, msg);
    }

    public Task<SetGroupSpecialTitleRes?> SetGroupSpecialTitle(SetGroupSpecialTitle msg)
    {
        return Post<SetGroupSpecialTitleRes>(SendUrl.SetGroupSpecialTitle, msg);
    }

    public Task<SetGroupWholeBanRes?> SetGroupWholeBan(SetGroupWholeBan msg)
    {
        return Post<SetGroupWholeBanRes>(SendUrl.SetGroupWholeBan, msg);
    }

    public Task<SetRestartRes?> SetRestart(SetRestart msg)
    {
        return Post<SetRestartRes>(SendUrl.SetRestart, msg);
    }

    public override void Dispose()
    {
        client.CancelPendingRequests();
        client.Dispose();
    }

    public override Task Start()
    {
        return Task.CompletedTask;
    }

    public override Task Close()
    {
        return Task.CompletedTask;
    }
}
