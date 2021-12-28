using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.SqlSugar
{
    /// <summary>
    /// 查询过滤器
    /// </summary>
    public interface IQueryFilter
    {
        /// <summary>
        /// 获取过滤条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<List<IConditionalModel>> GetConditions<TEntity>();

        /// <summary>
        /// 获取过滤条件(含查询模型配置的条件)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<List<IConditionalModel>> GetConditions<TEntity, TQuery>(TQuery query);
    }
}
