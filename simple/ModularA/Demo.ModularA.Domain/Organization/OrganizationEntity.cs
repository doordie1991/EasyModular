using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ModularA.Domain.Organization
{
    [SugarTable("Sys_Organization")]
    public partial class OrganizationEntity: EntityBase
    {
        /// <summary>
        /// 组织编码
        /// </summary>
        public string OrganizeCode { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrganizeName { get; set; }

        /// <summary>
        /// 父级编码
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
