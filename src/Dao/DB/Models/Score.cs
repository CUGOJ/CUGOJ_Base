using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 得分表
    /// </summary>
    public partial class Score
    {
        /// <summary>
        /// ScoreID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// Score名称
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 目标主体类型
        /// </summary>
        public int TargetType { get; set; }
        /// <summary>
        /// 目标主体ID
        /// </summary>
        public ulong TargetId { get; set; }
        /// <summary>
        /// 聚合基准
        /// </summary>
        public ulong AggId { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public ulong Value { get; set; }
        /// <summary>
        /// 状态枚举
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
    }
}
