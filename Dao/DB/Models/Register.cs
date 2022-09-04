using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 比赛注册表
    /// </summary>
    public partial class Register
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// 比赛ID
        /// </summary>
        public ulong ContestId { get; set; }
        /// <summary>
        /// 注册人ID
        /// </summary>
        public ulong RegistrantId { get; set; }
        /// <summary>
        /// 注册人类型
        /// </summary>
        public uint RegistrantType { get; set; }
        /// <summary>
        /// 队伍ID
        /// </summary>
        public ulong? TeamId { get; set; }
        /// <summary>
        /// 额外信息
        /// </summary>
        public string? Extra { get; set; }
        /// <summary>
        /// 比赛状态枚举
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
