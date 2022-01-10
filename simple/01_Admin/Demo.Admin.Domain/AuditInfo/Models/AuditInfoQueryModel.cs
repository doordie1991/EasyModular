
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 审计信息表查询模型
    /// </summary>
    public class AuditInfoQueryModel : QueryPagingModel
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
        /// 区域
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Area { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Controller { get; set; }

        /// <summary>
        /// 操作时间(开始)
        /// </summary>
        [Condition(FieldName= "CreatedTime", ConditionalType = ConditionalType.GreaterThanOrEqual)]
        public DateTime OperateTimeStart { get; set; }

        private DateTime operateTimeEnd;
        /// <summary>
        /// 操作时间(结束)
        /// </summary> 
        [Condition(FieldName = "CreatedTime", ConditionalType = ConditionalType.LessThan)]
        public DateTime? OperateTimeEnd
        {
            get
            {
                return operateTimeEnd;
            }
            set
            {
                if (value != DateTime.MinValue) operateTimeEnd = Convert.ToDateTime(value.ToString()).AddDays(1);
            }
        }
    }
}
