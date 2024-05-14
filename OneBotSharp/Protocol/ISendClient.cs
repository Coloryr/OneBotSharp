using OneBotSharp.Objs.Api;

namespace OneBotSharp.Protocol;

public static class SendUrl
{
    public const string SendPrivateMsg = "/send_private_msg";
    public const string SendGroupMsg = "/send_group_msg";
    public const string SendMsg = "/send_msg";
    public const string DeleteMsg = "/delete_msg";
    public const string GetMsg = "/get_msg";
    public const string GetForwardMsg = "/get_forward_msg";
    public const string SendLike = "/send_like";
    public const string SetGroupKick = "/set_group_kick";
    public const string SetGroupBan = "/set_group_ban";
    public const string SetGroupAnonymousBan = "/set_group_anonymous_ban";
    public const string SetGroupWholeBan = "set_group_whole_ban";
    public const string SetGroupAdmin = "/set_group_admin";
    public const string SetGroupAnonymous = "/set_group_anonymous";
    public const string SetGroupCard = "/set_group_card";
    public const string SetGroupName = "/set_group_name";
    public const string SetGroupLeave = "/set_group_leave";
    public const string SetGroupSpecialTitle = "/set_group_special_title";
    public const string SetFriendAddRequest = "/set_friend_add_request";
    public const string SetGroupAddRequest = "/set_group_add_request";
    public const string GetLoginInfo = "/get_login_info";
    public const string GetStrangerInfo = "/get_stranger_info";
    public const string GetFriendList = "/get_friend_list";
    public const string GetGroupInfo = "/get_group_info";
    public const string GetGroupList = "/get_group_list";
    public const string GetGroupMemberInfo = "/get_group_member_info";
    public const string GetGroupMemberList = "/get_group_member_list";
    public const string GetGroupHonorInfo = "/get_group_honor_info";
    public const string GetCookies = "/get_cookies";
    public const string GetCsrfToken = "/get_csrf_token";
    public const string GetCredentials = "/get_credentials";
    public const string GetRecord = "/get_record";
    public const string GetImage = "/get_image";
    public const string CanSendImage = "/can_send_image";
    public const string CanSendRecord = "/can_send_record";
    public const string GetStatus = "/get_status";
    public const string GetVersionInfo = "/get_version_info";
    public const string SetRestart = "/set_restart";
    public const string CleanCache = "/clean_cache";
}

public interface ISendClient
{
    /// <summary>
    /// 发送私聊消息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SendPrivateMsgRes?> SendPrivateMsg(SendPrivateMsg msg);
    /// <summary>
    /// 发送群消息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SendGroupMsgRes?> SendGroupMsg(SendGroupMsg msg);
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SendMsgRes?> SendMsg(SendMsg msg);
    /// <summary>
    /// 撤回消息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<DeleteMsgRes?> DeleteMsg(DeleteMsg msg);
    /// <summary>
    /// 获取消息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<GetMsgRes?> GetMsg(GetMsg msg);
    /// <summary>
    /// 获取合并转发消息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<GetForwardMsgRes?> GetForwardMsg(GetForwardMsg msg);
    /// <summary>
    /// 发送好友赞
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SendLikeRes?> SendLike(SendLike msg);
    /// <summary>
    /// 群组踢人
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetGroupKickRes?> SetGroupKick(SetGroupKick msg);
    /// <summary>
    /// 群组单人禁言
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetGroupBanRes?> SetGroupBan(SetGroupBan msg);
    /// <summary>
    /// 群组匿名用户禁言
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetGroupAnonymousBanRes?> SetGroupAnonymousBan(SetGroupAnonymousBan msg);
    /// <summary>
    /// 群组全员禁言
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetGroupWholeBanRes?> SetGroupWholeBan(SetGroupWholeBan msg);
    /// <summary>
    /// 群组设置管理员
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetGroupAdminRes?> SetGroupAdmin(SetGroupAdmin msg);
    /// <summary>
    /// 群组匿名
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetGroupAnonymousRes?> SetGroupAnonymous(SetGroupAnonymous msg);
    /// <summary>
    /// 设置群名片（群备注）
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetGroupCardRes?> SetGroupCard(SetGroupCard msg);
    /// <summary>
    /// 设置群名
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetGroupNameRes?> SetGroupName(SetGroupName msg);
    /// <summary>
    /// 退出群组
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetGroupLeaveRes?> SetGroupLeave(SetGroupLeave msg);
    /// <summary>
    /// 设置群组专属头衔
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetGroupSpecialTitleRes?> SetGroupSpecialTitle(SetGroupSpecialTitle msg);
    /// <summary>
    /// 处理加好友请求
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetFriendAddRequestRes?> SetFriendAddRequest(SetFriendAddRequest msg);
    /// <summary>
    /// 处理加群请求／邀请
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetGroupAddRequestRes?> SetGroupAddRequest(SetGroupAddRequest msg);
    /// <summary>
    /// 获取登录号信息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<GetLoginInfoRes?> GetLoginInfo();
    /// <summary>
    /// 获取陌生人信息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<GetStrangerInfoRes?> GetStrangerInfo(GetStrangerInfo msg);
    /// <summary>
    /// 获取好友列表
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<List<GetFriendListRes>?> GetFriendList();
    /// <summary>
    /// 获取群信息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<GetGroupInfoRes?> GetGroupInfo(GetGroupInfo msg);
    /// <summary>
    /// 获取群列表
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<List<GetGroupListRes>?> GetGroupList();
    /// <summary>
    /// 获取群成员信息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<GetGroupMemberInfoRes?> GetGroupMemberInfo(GetGroupMemberInfo msg);
    /// <summary>
    /// 获取群成员列表
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<GetGroupMemberListRes?> GetGroupMemberList(GetGroupMemberList msg);
    /// <summary>
    /// 获取群荣誉信息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<GetGroupHonorInfoRes?> GetGroupHonorInfo(GetGroupHonorInfo msg);
    /// <summary>
    /// 获取 Cookies
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<GetCookiesRes?> GetCookies(GetCookies msg);
    /// <summary>
    /// 获取 CSRF Token
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<GetCsrfTokenRes?> GetCsrfToken();
    /// <summary>
    /// 获取 QQ 相关接口凭证
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<GetCredentialsRes?> GetCredentials(GetCredentials msg);
    /// <summary>
    /// 获取语音
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    [Obsolete("不推荐使用")]
    public Task<GetRecordRes?> GetRecord(GetRecord msg);
    /// <summary>
    /// 获取图片
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    [Obsolete("不推荐使用")]
    public Task<GetImageRes?> GetImage(GetImage msg);
    /// <summary>
    /// 检查是否可以发送图片
    /// </summary>
    /// <returns></returns>
    public Task<bool?> CanSendImage();
    /// <summary>
    /// 检查是否可以发送语音
    /// </summary>
    /// <returns></returns>
    public Task<bool?> CanSendRecord();
    /// <summary>
    /// 获取运行状态
    /// </summary>
    /// <returns></returns>
    public Task<GetStatusRes?> GetStatus();
    /// <summary>
    /// 获取版本信息
    /// </summary>
    /// <returns></returns>
    public Task<GetVersionInfoRes?> GetVersionInfo();
    /// <summary>
    /// 重启 OneBot 实现
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task<SetRestartRes?> SetRestart(SetRestart msg);
    /// <summary>
    /// 清理缓存
    /// </summary>
    /// <returns></returns>
    public Task CleanCache();
}
