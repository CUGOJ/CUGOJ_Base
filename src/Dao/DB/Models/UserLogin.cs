using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 用户登录记录表
    /// </summary>
    public partial class UserLogin
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public ulong UserId { get; set; }
        /// <summary>
        /// 登录IP
        /// </summary>
        public ulong Ip { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; } = null!;
        /// <summary>
        /// 平台
        /// </summary>
        public uint Platform { get; set; }
        /// <summary>
        /// 登录类型
        /// </summary>
        public uint LoginType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
