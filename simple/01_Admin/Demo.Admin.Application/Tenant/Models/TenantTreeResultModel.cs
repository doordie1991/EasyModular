using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 租户返回列表模型
    /// </summary>
    public class TenantTreeResultModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 租户编码
        /// </summary>
        public string TenantCode { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }
    }
}
