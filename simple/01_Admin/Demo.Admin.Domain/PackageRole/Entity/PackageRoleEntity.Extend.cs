using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public partial class PackageRoleEntity
    {
        /// <summary>
        /// 角色编码
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string RoleCode { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string RoleName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Remark { get; set; }
    }
}
