using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.Auth
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public interface ILoginInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// 用户编码
        /// </summary>
        string UserCode { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// 组织Id
        /// </summary>
        string OrganizeId { get; }

        /// <summary>
        /// 租户Id
        /// </summary>
        string TenantId { get; }

        /// <summary>
        /// 租户名称
        /// </summary>
        string TenantName { get; }

        /// <summary>
        /// 租户类别
        /// </summary>
        string TenantType { get; }

        /// <summary>
        /// 行业
        /// </summary>
        string Trade { get; }

        /// <summary>
        /// 角色Id
        /// </summary>
        string RoleIds { get; }

        /// <summary>
        /// 角色编码
        /// </summary>
        string RoleCodes { get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        string RoleNames { get; }

        /// <summary>
        /// 获取当前用户IP(包含IPv和IPv6)
        /// </summary>
        string IP { get; }

        /// <summary>
        /// 获取当前用户IPv4
        /// </summary>
        string IPv4 { get; }

        /// <summary>
        /// 获取当前用户IPv6
        /// </summary>
        string IPv6 { get; }

        /// <summary>
        /// 登录时间戳
        /// </summary>
        long LoginTime { get; }


    }
}
