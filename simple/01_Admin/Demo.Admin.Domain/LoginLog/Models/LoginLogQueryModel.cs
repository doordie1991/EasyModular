
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 登录日志查询模型
    /// </summary>
    public class LoginLogQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string UserCode { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string UserName { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public bool? Result { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string BrowserInfo { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string IP { get; set; }

        /// <summary>
        /// 登录时间(开始)
        /// </summary>
        [Condition(FieldName = "CreatedTime", ConditionalType = ConditionalType.GreaterThanOrEqual)]
        public DateTime LoginTimeStart { get; set; }


        private DateTime loginTimeEnd;
        /// <summary>
        /// 登录时间(结束)
        /// </summary> 
        [Condition(FieldName = "CreatedTime", ConditionalType = ConditionalType.LessThan)]
        public DateTime? LoginTimeEnd
        {
            get
            {
                return loginTimeEnd;
            }
            set
            {
                if (value != DateTime.MinValue) loginTimeEnd = Convert.ToDateTime(value.ToString()).AddDays(1);
            }
        }

    }
}
