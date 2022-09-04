using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 题单表
    /// </summary>
    public partial class Problemset
    {
        /// <summary>
        /// 题单ID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// 题单名称
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// 创建者ID
        /// </summary>
        public ulong CreatorId { get; set; }
        /// <summary>
        /// 简短描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 状态枚举
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
