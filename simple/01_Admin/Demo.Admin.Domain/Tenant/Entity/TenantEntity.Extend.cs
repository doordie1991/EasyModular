using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public partial class TenantEntity
    {

        /// <summary>
        /// 租户类别名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string TenantTypeName { get; set; }

        /// <summary>
        /// 套餐
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Package { get; set; }

    }
}
