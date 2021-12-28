using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Utils
{
    /// <summary>
    /// 禁用审计功能
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableAuditingAttribute : Attribute
    {

    }
}
