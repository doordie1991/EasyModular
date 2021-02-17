using EasyModular.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ModularA.Domain.Organization.Models
{
    public class OrganizationQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 组织编码
        /// </summary>
        public string OrganizeCode { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrganizeName { get; set; }
    }
}
