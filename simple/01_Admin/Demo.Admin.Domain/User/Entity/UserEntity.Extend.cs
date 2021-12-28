using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    public partial class UserEntity
    {
        /// <summary>
        /// 组织全称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string OrganizeFullName { get; set; }
    }
}
