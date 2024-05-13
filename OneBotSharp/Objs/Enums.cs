using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBotSharp.Objs;

public static class Enums
{
    public static class MsgType
    {
        public const string Anonymous = "anonymous";
        public const string At = "at";
        public const string Contact = "contact";
        public const string Dice = "dice";
        public const string Face = "face";
        public const string Forward = "forward";
        public const string Image = "image";
        public const string Json = "json";
        public const string Location = "location";
        public const string Music = "music";
        public const string Node = "node";
        public const string Poke = "poke";
        public const string Record = "record";
        public const string Reply = "reply";
        public const string Rps = "rps";
        public const string Shake = "shake";
        public const string Share = "share";
        public const string Text = "text";
        public const string Video = "video";
        public const string Xml = "xml";
    }

    public static class ContactType
    {
        public const string Friend = "qq";
        public const string Group = "group";
    }

    public static class ImageType
    {
        public const string Flash = "flash";
    }

    public static class MusicType
    {
        public const string QQ = "qq";
        public const string N163 = "163";
        public const string XM = "xm";
        public const string Custom = "custom";
    }

    public static class EventType
    {
        public const string Message = "message";
        public const string Notice = "notice";
        public const string Request = "request";
        public const string Meta = "meta_event";
    }

    public static class MessageType
    {
        public const string Private = "private";
        public const string Group = "group";
    }

    public static class SexType
    {
        public const string Male = "male";
        public const string Female = "female";
        public const string Unknown = "unknown";
    }

    public static class RoleType
    {
        public const string Owner = "owner";
        public const string Admin = "admin";
        public const string Member = "member";
    }

    public static class MetaType
    {
        public const string Lifecycle = "lifecycle";
        public const string Heartbeat = "heartbeat";
    }

    public static class LifecycleType
    {
        public const string SubTypeEnable = "enable";
        public const string SubTypeDisable = "disable";
        public const string SubTypeConnect = "connect";
    }

    public static class NoticeType
    {
        public const string GroupUpload = "group_upload";
        public const string GroupAdmin = "group_admin";
        public const string GroupDecrease = "group_decrease";
        public const string GroupIncrease = "group_increase";
        public const string GroupBan = "group_ban";
        public const string FriendAdd = "friend_add";
        public const string GroupRecall = "group_recall";
        public const string FriendRecall = "friend_recall";
        public const string GroupNotify = "notify";
        public const string GroupLuckyKing = "lucky_king";
    }

    public static class SetType
    {
        public const string SubTypeSet = "set";
        public const string SubTypeUnset = "unset";
    }

    public static class GroupBanType
    {
        public const string Ban = "ban";
        public const string LiftBan = "lift_ban";
    }

    public static class GroupDecreaseType
    {
        public const string Leave = "leave";
        public const string Kick = "kick";
        public const string KickMe = "kick_me";
    }

    public static class GroupIncreaseType
    {
        public const string Approve = "approve";
        public const string Invite = "invite";
    }

    public static class GroupNotifyType
    {
        public const string Poke = "poke";
        public const string Honor = "honor";
    }

    public static class HonorType
    {
        public const string Talkative = "talkative";
        public const string Performer = "performer";
        public const string Emotion = "emotion";
    }

    public static class RequestType
    {
        public const string Friend = "friend";
        public const string Group = "group";
    }

    public static class RequestGroupType
    {
        public const string Add = "add";
        public const string Invite = "invite";
    }

    public static class GroupHonorType
    {
        /// <summary>
        /// 龙王
        /// </summary>
        public const string Talkative = "talkative";
        /// <summary>
        /// 群聊之火
        /// </summary>
        public const string Performer = "performer";
        /// <summary>
        /// 群聊炽焰
        /// </summary>
        public const string Legend = "legend";
        /// <summary>
        /// 冒尖小春笋
        /// </summary>
        public const string StrongNewbie = "strong_newbie";
        /// <summary>
        /// 快乐之源
        /// </summary>
        public const string Emotion = "emotion";
        /// <summary>
        /// 全部
        /// </summary>
        public const string All = "all";
    }
}