﻿using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 题解内容表
    /// </summary>
    public partial class SolutionContent
    {
        /// <summary>
        /// 题解内容ID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// 题解ID
        /// </summary>
        public ulong SolutionId { get; set; }
        /// <summary>
        /// 题解内容
        /// </summary>
        public string? Content { get; set; }
    }
}