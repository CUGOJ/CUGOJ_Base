using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 题目内容
    /// </summary>
    public partial class ProblemContent
    {
        /// <summary>
        /// 题目内容ID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// 题目ID
        /// </summary>
        public ulong ProblemId { get; set; }
        /// <summary>
        /// 题目具体内容
        /// </summary>
        public string? Content { get; set; }
    }
}
