using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 赛题列表
    /// </summary>
    public partial class ContestProblem
    {
        /// <summary>
        /// 赛题ID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// 比赛ID
        /// </summary>
        public ulong ContestId { get; set; }
        /// <summary>
        /// 题目ID
        /// </summary>
        public ulong ProblemId { get; set; }
        /// <summary>
        /// 提交数
        /// </summary>
        public ulong SubmissionCount { get; set; }
        /// <summary>
        /// AC数
        /// </summary>
        public ulong AcceptedCount { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public ulong Version { get; set; }
        /// <summary>
        /// 状态枚举
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 分数、语言等信息的JSON格式
        /// </summary>
        public string? Properties { get; set; }
    }
}
