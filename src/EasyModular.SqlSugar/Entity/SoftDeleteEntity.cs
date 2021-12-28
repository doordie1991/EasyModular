using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.SqlSugar
{
    /// <summary>
    /// 软删除实体模型
    /// </summary>
    public class SoftDeleteEntity<IdKey> : BaseEntity<IdKey>
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>           
        public string Deleter { get; set; }

        /// <summary>
        /// 删除人姓名
        /// </summary>           
        public string DeleterName { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>           
        public DateTime? DeletedTime { get; set; }
    }
}
