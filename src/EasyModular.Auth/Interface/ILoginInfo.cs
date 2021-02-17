using System;
using System.Collections.Generic;
using System.Text;

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
        Guid UserId { get; }

        /// <summary>
        /// 用户编码
        /// </summary>
        string UserCode { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// 组织编号
        /// </summary>
        string OrganizeCode { get; }

        /// <summary>
        /// 组织名称
        /// </summary>
        string OrganizeName { get; }

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
