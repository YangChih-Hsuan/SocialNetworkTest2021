using System;

namespace SocialNetworkTest2021.Enum
{
    /// <summary>
    /// 會員狀態
    /// </summary>
    public enum MemberStatusEnum
    {
        在線 = 1,
        忙碌 = 2,
        離線 = 3
    }

    /// <summary>
    /// 會員資訊狀態
    /// </summary>
    [Flags]
    public enum MemberInfoEnum
    {
        生日 = 1,
        興趣 = 2,
        工作 = 4,
        學歷 = 8,

        All = 生日 | 興趣 | 工作 | 學歷
    }
}