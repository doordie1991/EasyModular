using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.SqlSugar
{
    /// <summary>
    /// 表基础模型
    /// </summary>
    public class BaseEntity<IdKey>: IEntity
    {
        /// <summary>
        /// Id
        /// </summary>  
        [SugarColumn(IsPrimaryKey = true)]
        public IdKey Id { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>           
        public string Creater { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>           
        public string CreaterName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>           
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>           
        public string Modifier { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>           
        public string ModifierName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>           
        public DateTime ModifiedTime { get; set; }

    }
}
