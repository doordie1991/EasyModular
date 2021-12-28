using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    public partial class DictionaryEntity
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string GroupName { get; set; }
    }
}
