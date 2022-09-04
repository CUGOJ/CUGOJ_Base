using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 组织信息表
    /// </summary>
    public partial class Organization
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// 组织名
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 组织所有人
        /// </summary>
        public ulong Owner { get; set; }
        /// <summary>
        /// 父组织
        /// </summary>
        public ulong ParentId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }
        /// <summary>
        /// 状态
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
