using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 用户移动模型
    /// </summary>
    public class UserMoveModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 源组织Id
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 目标组织Id
        /// </summary>
        public string TargetId { get; set; }
    }
}
