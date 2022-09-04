using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 提交基本信息表
    /// </summary>
    public partial class SubmissionBase
    {
        /// <summary>
        /// 提交ID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmitTime { get; set; }
        /// <summary>
        /// 提交者ID
        /// </summary>
        public ulong SubmitterId { get; set; }
        /// <summary>
        /// 提交者类型（团队或个人）
        /// </summary>
        public uint SubmitterType { get; set; }
        /// <summary>
        /// 提交结果
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 提交类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 关联的比赛
        /// </summary>
        public ulong? ContestId { get; set; }
        /// <summary>
        /// 关联的题目
        /// </summary>
        public ulong? ProblemId { get; set; }
        /// <summary>
        /// 特定配置JSON
        /// </summary>
        public string? Properties { get; set; }
    }
}
