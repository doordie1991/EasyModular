using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Domain
{
    public partial class JobEntity
    {
        /// <summary>
        /// 任务分组
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string GroupName { get; set; }
    }
}
