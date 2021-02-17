using EasyModular.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ModularB.Domain.User.Models
{
    public class UserQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
    }
}
