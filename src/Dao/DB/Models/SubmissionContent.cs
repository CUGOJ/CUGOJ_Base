using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 提交内容表
    /// </summary>
    public partial class SubmissionContent
    {
        /// <summary>
        /// 提交内容ID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// 提交ID
        /// </summary>
        public ulong SubmissionId { get; set; }
        /// <summary>
        /// 提交内容
        /// </summary>
        public string? Content { get; set; }
    }
}
