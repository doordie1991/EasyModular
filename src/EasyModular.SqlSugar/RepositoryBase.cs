using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.SqlSugar
{
    /// <summary>
    /// 仓储抽象类
    /// </summary>
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, new()
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected readonly IDbContext DbContext;

        protected RepositoryBase(IDbContext context)
        {
            DbContext = context;
        }

        #region ==Exists==
        public virtual bool Exists(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : (isNewConnection ? DbContext.GetInstance() : DbContext.Db)).Queryable<T>().Any(whereExpression);
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().AnyAsync(whereExpression);
        }

        public virtual bool ExistsById(dynamic id, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().InSingle(id) != null;
        }

        public virtual Task<bool> ExistsByIdAsync(dynamic id, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().InSingleAsync(id) != null;
        }
        #endregion

        #region ==Insert==
        public virtual bool Insert(T entity, bool isNewConnection = true)
        {
            if (entity == null)
                return false;

            SetCreate(entity);

            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Insertable(entity).ExecuteCommand() > 0;
        }

        public virtual Task<bool> InsertAsync(T entity, bool isNewConnection = true)
        {
            if (entity == null)
                return Task.FromResult(false);

            SetCreate(entity);

            return Task.FromResult((isNewConnection ? DbContext.GetInstance() : DbContext.Db).Insertable(entity).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool InsertRange(List<T> list, bool isNewConnection = true)
        {
            BatchSetCreate(list);

            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Insertable(list).ExecuteCommand() > 0;
        }

        public virtual Task<bool> InsertRangeAsync(List<T> list, bool isNewConnection = true)
        {
            BatchSetCreate(list);

            return Task.FromResult((isNewConnection ? DbContext.GetInstance() : DbContext.Db).Insertable(list).ExecuteCommandAsync().Result > 0);
        }

        #endregion

        #region ==Delete==

        public virtual bool DeleteById(dynamic id, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Deleteable<T>().In(id).ExecuteCommand() > 0;
        }

        public virtual Task<bool> DeleteByIdAsync(dynamic id, bool isNewConnection = true)
        {
            return Task.FromResult((isNewConnection ? DbContext.GetInstance() : DbContext.Db).Deleteable<T>().In(id).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool DeleteByIds(dynamic[] ids, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Deleteable<T>().In(ids).ExecuteCommand() > 0;
        }

        public virtual Task<bool> DeleteByIdsAsync(dynamic[] ids, bool isNewConnection = true)
        {
            return Task.FromResult((isNewConnection ? DbContext.GetInstance() : DbContext.Db).Deleteable<T>().In(ids).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool Delete(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Deleteable<T>().Where(whereExpression).ExecuteCommand() > 0;
        }

        public virtual Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return Task.FromResult((isNewConnection ? DbContext.GetInstance() : DbContext.Db).Deleteable<T>().Where(whereExpression).ExecuteCommandAsync().Result > 0);
        }
        #endregion

        #region ==SoftDelete==
        public virtual bool SoftDelete(T entity, bool isNewConnection = true)
        {
            SetDelete(entity);
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Updateable(entity).ExecuteCommand() > 0;
        }

        public virtual Task<bool> SoftDeleteAsync(T entity, bool isNewConnection = true)
        {
            SetDelete(entity);
            return Task.FromResult((isNewConnection ? DbContext.GetInstance() : DbContext.Db).Updateable(entity).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool SoftDeleteRange(T[] entitys, bool isNewConnection = true)
        {
            BatchSetDelete(entitys);
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Updateable(entitys).ExecuteCommand() > 0;
        }

        public virtual Task<bool> SoftDeleteRangeAsync(T[] entitys, bool isNewConnection = true)
        {
            BatchSetDelete(entitys);
            return Task.FromResult((isNewConnection ? DbContext.GetInstance() : DbContext.Db).Updateable(entitys).ExecuteCommandAsync().Result > 0);
        }
        #endregion

        #region ==Update==
        public virtual bool Update(T entity, bool isNewConnection = true)
        {
            SetModify(entity);
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Updateable(entity).ExecuteCommand() > 0;
        }

        public virtual Task<bool> UpdateAsync(T entity, bool isNewConnection = true)
        {
            SetModify(entity);
            return Task.FromResult((isNewConnection ? DbContext.GetInstance() : DbContext.Db).Updateable(entity).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool Update(Expression<Func<T, object>> columns, Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Updateable<T>().UpdateColumns(columns).Where(whereExpression).ExecuteCommand() > 0;
        }

        public virtual Task<bool> UpdateAsync(Expression<Func<T, object>> columns, Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return Task.FromResult((isNewConnection ? DbContext.GetInstance() : DbContext.Db).Updateable<T>().UpdateColumns(columns).Where(whereExpression).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool UpdateRange(T[] entitys, bool isNewConnection = true)
        {
            BatchSetModify(entitys);
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Updateable(entitys).ExecuteCommand() > 0;
        }

        public virtual Task<bool> UpdateRangeAsync(T[] entitys, bool isNewConnection = true)
        {
            BatchSetModify(entitys);
            return Task.FromResult((isNewConnection ? DbContext.GetInstance() : DbContext.Db).Updateable(entitys).ExecuteCommandAsync().Result > 0);
        }
        #endregion

        #region ==Get==

        public virtual T Get(dynamic id, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().InSingle(id);
        }

        public virtual Task<T> GetAsync(dynamic id, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().InSingleAsync(id);
        }

        protected virtual T Get(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().Single(whereExpression);
        }

        protected virtual Task<T> GetAsync(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().SingleAsync(whereExpression);
        }

        public virtual List<T> GetList(bool isNewConnection = true)
        {
            return DbContext.GetInstance().Queryable<T>().ToList();
        }

        public virtual Task<List<T>> GetListAsync(bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().ToListAsync();
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().Where(whereExpression).ToList();
        }

        public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().Where(whereExpression).ToListAsync();
        }

        public virtual int GetCount(bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().Count();
        }

        public virtual Task<int> GetCountAsync(bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().CountAsync();
        }

        public virtual int GetCount(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().Where(whereExpression).Count();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<T, bool>> whereExpression, bool isNewConnection = true)
        {
            return (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().Where(whereExpression).CountAsync();
        }
        #endregion

        #region ==PageList==
        public List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page, bool isNewConnection = true)
        {
            int totalNumber = 0;
            List<T> result = (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().Where(whereExpression).ToPageList(page.PageIndex, page.PageSize, ref totalNumber);
            page.PageCount = totalNumber;
            return result;
        }

        public List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc, bool isNewConnection = true)
        {
            int totalNumber = 0;
            List<T> result = (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression)
                .ToPageList(page.PageIndex, page.PageSize, ref totalNumber);
            page.PageCount = totalNumber;
            return result;
        }

        public List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page, bool isNewConnection = true)
        {
            int totalNumber = 0;
            List<T> result = (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().Where(conditionalList).ToPageList(page.PageIndex, page.PageSize, ref totalNumber);
            page.PageCount = totalNumber;
            return result;
        }

        public List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc, bool isNewConnection = true)
        {
            int totalNumber = 0;
            List<T> result = (isNewConnection ? DbContext.GetInstance() : DbContext.Db).Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(conditionalList)
                .ToPageList(page.PageIndex, page.PageSize, ref totalNumber);
            page.PageCount = totalNumber;
            return result;
        }
        #endregion

        #region ==Private==
        /// <summary>
        /// 设置创建人、修改人、创建时间、修改时间
        /// </summary>
        /// <param name="entity"></param>
        private void SetCreate(T entity, DateTime? dtNow = null)
        {
            dtNow = dtNow ?? DateTime.Now;

            foreach (var item in entity.GetType().GetProperties())
            {
                if (item.Name == "Creater" || item.Name == "Modifier")
                {
                    item.SetValue(entity, DbContext.LoginInfo?.UserCode);
                }

                if (item.Name == "CreaterName" || item.Name == "ModifierName")
                {
                    item.SetValue(entity, DbContext.LoginInfo?.UserName);
                }

                if (item.Name == "CreatedTime" || item.Name == "ModifiedTime")
                {
                    item.SetValue(entity, dtNow);
                }
            }
        }

        /// <summary>
        /// 批量设置创建人、修改人、创建时间、修改时间
        /// </summary>
        /// <param name="list"></param>
        private void BatchSetCreate(List<T> list)
        {
            var dtNow = DateTime.Now;
            list.ForEach(m => { SetCreate(m, dtNow); });
        }

        /// <summary>
        /// 设置修改人、修改时间
        /// </summary>
        /// <param name="entity"></param>
        private void SetModify(T entity, DateTime? dtNow = null)
        {
            dtNow = dtNow ?? DateTime.Now;

            foreach (var item in entity.GetType().GetProperties())
            {
                if (item.Name == "Modifier")
                {
                    item.SetValue(entity, DbContext.LoginInfo?.UserCode);
                }

                if (item.Name == "ModifierName")
                {
                    item.SetValue(entity, DbContext.LoginInfo?.UserName);
                }

                if (item.Name == "ModifiedTime")
                {
                    item.SetValue(entity, DateTime.Now);
                }
            }
        }

        /// <summary>
        /// 批量设置修改人、修改时间
        /// </summary>
        /// <param name="list"></param>
        private void BatchSetModify(T[] list)
        {
            var dtNow = DateTime.Now;
            foreach (var item in list)
            {
                SetModify(item, dtNow);
            }
        }

        /// <summary>
        /// 设置删除人、删除状态、删除时间
        /// </summary>
        /// <param name="entity"></param>
        private void SetDelete(T entity, DateTime? dtNow = null)
        {
            dtNow = dtNow ?? DateTime.Now;

            foreach (var item in entity.GetType().GetProperties())
            {
                if (item.Name == "Deleter")
                {
                    item.SetValue(entity, DbContext.LoginInfo?.UserCode);
                }

                if (item.Name == "DeleterName")
                {
                    item.SetValue(entity, DbContext.LoginInfo?.UserName);
                }

                if (item.Name == "DeletedTime")
                {
                    item.SetValue(entity, DateTime.Now);
                }

                if (item.Name == "IsDel")
                {
                    item.SetValue(entity, true);
                }
            }
        }

        /// <summary>
        /// 批量设置删除人、删除状态、删除时间
        /// </summary>
        /// <param name="list"></param>
        private void BatchSetDelete(T[] list)
        {
            var dtNow = DateTime.Now;
            foreach (var item in list)
            {
                SetDelete(item, dtNow);
            }
        }
        #endregion

    }
}
