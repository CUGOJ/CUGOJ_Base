using System;
using System.Collections.Generic;

namespace CUGOJ.Base.Dao.DB.Models
{
    /// <summary>
    /// 比赛文字内容列表
    /// </summary>
    public partial class ContestContent
    {
        /// <summary>
        /// 比赛内容ID
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// 比赛ID
        /// </summary>
        public ulong ContestId { get; set; }
        /// <summary>
        /// 赛事描述文字
        /// </summary>
        public string? Content { get; set; }
    }
}
