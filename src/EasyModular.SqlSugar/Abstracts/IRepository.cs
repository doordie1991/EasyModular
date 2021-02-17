﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.SqlSugar
{
    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, new()
    {
        #region ==Exists==

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true);

        /// <summary>
        /// 根据id判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ExistsById(dynamic id, bool isNewConnection = true);

        /// <summary>
        /// 根据id判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsByIdAsync(dynamic id, bool isNewConnection = true);

        #endregion

        #region ==Insert==

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Insert(T entity, bool isNewConnection = true);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> InsertAsync(T entity, bool isNewConnection = true);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns></returns>
        bool InsertRange(List<T> list, bool isNewConnection = true);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns></returns>
        Task<bool> InsertRangeAsync(List<T> list, bool isNewConnection = true);

        #endregion

        #region ==Delete==
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteById(dynamic id, bool isNewConnection = true);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(dynamic id, bool isNewConnection = true);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteByIds(dynamic[] ids, bool isNewConnection = true);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdsAsync(dynamic[] ids, bool isNewConnection = true);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true);
        #endregion

        #region ==SoftDelete==
        bool SoftDelete(T entity, bool isNewConnection = true);

        Task<bool> SoftDeleteAsync(T entity, bool isNewConnection = true);

        bool SoftDeleteRange(T[] entitys, bool isNewConnection = true);

        Task<bool> SoftDeleteRangeAsync(T[] entitys, bool isNewConnection = true);
   
        #endregion

        #region ==Update==
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Update(T entity, bool isNewConnection = true);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity, bool isNewConnection = true);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="columns">更新列</param>
        /// <param name="columns">更新条件</param>
        /// <returns></returns>
        bool Update(Expression<Func<T, object>> columns, Expression<Func<T, bool>> whereExpression, bool isNewConnection = true);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="columns">更新列</param>
        /// <param name="columns">更新条件</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Expression<Func<T, object>> columns, Expression<Func<T, bool>> whereExpression, bool isNewConnection = true);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool UpdateRange(T[] entitys, bool isNewConnection = true);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> UpdateRangeAsync(T[] entitys, bool isNewConnection = true);

        #endregion

        #region ==Get==
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(dynamic id, bool isNewConnection = true);

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(dynamic id, bool isNewConnection = true);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        List<T> GetList(bool isNewConnection = true);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetListAsync(bool isNewConnection = true);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true);

        /// <summary>
        /// 获取行数
        /// </summary>
        /// <returns></returns>
        int GetCount(bool isNewConnection = true);

        /// <summary>
        /// 获取行数
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync(bool isNewConnection = true);

        /// <summary>
        /// 获取行数
        /// </summary>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true);

        /// <summary>
        /// 获取行数
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true);

        #endregion

        #region ==PageList==
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page, bool isNewConnection = true);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="page"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc, bool isNewConnection = true);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="conditionalList"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page, bool isNewConnection = true);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="conditionalList"></param>
        /// <param name="page"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc, bool isNewConnection = true);
        #endregion
    }
}
