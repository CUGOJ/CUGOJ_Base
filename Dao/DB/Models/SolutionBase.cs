using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 题解基本信息表
    /// </summary>
    public partial class SolutionBase
    {
        /// <summary>
        /// 题解ID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// 作者ID
        /// </summary>
        public ulong WriterId { get; set; }
        /// <summary>
        /// 关联的比赛
        /// </summary>
        public ulong? ContestId { get; set; }
        /// <summary>
        /// 关联的题目
        /// </summary>
        public ulong? ProblemId { get; set; }
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
