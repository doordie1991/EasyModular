using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.SqlSugar
{
    /// <summary>
    /// 表基础模型
    /// </summary>
    public class EntityBase: IEntity
    {
        /// <summary>
        /// Id
        /// </summary>  
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; } = Guid.NewGuid();

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
