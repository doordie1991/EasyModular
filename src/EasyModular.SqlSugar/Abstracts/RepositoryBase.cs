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
        protected readonly IDbContext _dbContext;

        /// <summary>
        /// 过滤器
        /// </summary>
        protected readonly IQueryFilter _filter;

        protected RepositoryBase(IDbContext context, IQueryFilter filter = null)
        {
            _dbContext = context;
            _filter = filter;
        }

        #region ==Exists==
        public virtual bool Exists(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().Any(whereExpression);
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().AnyAsync(whereExpression);
        }

        public virtual bool ExistsById(dynamic id, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().InSingle(id) != null;
        }

        public virtual Task<bool> ExistsByIdAsync(dynamic id, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().InSingleAsync(id) != null;
        }
        #endregion

        #region ==Insert==
        public virtual bool Insert(T entity, SqlSugarScope Db = null)
        {
            if (entity == null)
                return false;

            SetCreate(entity);

            return (Db ?? _dbContext.GetInstance()).Insertable(entity).ExecuteCommand() > 0;
        }

        public virtual Task<bool> InsertAsync(T entity, SqlSugarScope Db = null)
        {
            if (entity == null)
                return Task.FromResult(false);

            SetCreate(entity);

            return Task.FromResult((Db ?? _dbContext.GetInstance()).Insertable(entity).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool InsertRange(List<T> list, SqlSugarScope Db = null)
        {
            BatchSetCreate(list);

            return (Db ?? _dbContext.GetInstance()).Insertable(list).ExecuteCommand() > 0;
        }

        public virtual Task<bool> InsertRangeAsync(List<T> list, SqlSugarScope Db = null)
        {
            BatchSetCreate(list);

            return Task.FromResult((Db ?? _dbContext.GetInstance()).Insertable(list).ExecuteCommandAsync().Result > 0);
        }

        #endregion

        #region ==Delete==

        public virtual bool DeleteById(dynamic id, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Deleteable<T>().In(id).ExecuteCommand() > 0;
        }

        public virtual Task<bool> DeleteByIdAsync(dynamic id, SqlSugarScope Db = null)
        {
            return Task.FromResult((Db ?? _dbContext.GetInstance()).Deleteable<T>().In(id).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool DeleteByIds(dynamic[] ids, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Deleteable<T>().In(ids).ExecuteCommand() > 0;
        }

        public virtual Task<bool> DeleteByIdsAsync(dynamic[] ids, SqlSugarScope Db = null)
        {
            return Task.FromResult((Db ?? _dbContext.GetInstance()).Deleteable<T>().In(ids).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool Delete(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Deleteable<T>().Where(whereExpression).ExecuteCommand() > 0;
        }

        public virtual Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return Task.FromResult((Db ?? _dbContext.GetInstance()).Deleteable<T>().Where(whereExpression).ExecuteCommandAsync().Result > 0);
        }
        #endregion

        #region ==SoftDelete==
        public virtual bool SoftDelete(T entity, SqlSugarScope Db = null)
        {
            SetDelete(entity);
            return (Db ?? _dbContext.GetInstance()).Updateable(entity).ExecuteCommand() > 0;
        }

        public virtual Task<bool> SoftDeleteAsync(T entity, SqlSugarScope Db = null)
        {
            SetDelete(entity);
            return Task.FromResult((Db ?? _dbContext.GetInstance()).Updateable(entity).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool SoftDeleteRange(T[] entitys, SqlSugarScope Db = null)
        {
            BatchSetDelete(entitys);
            return (Db ?? _dbContext.GetInstance()).Updateable(entitys).ExecuteCommand() > 0;
        }

        public virtual Task<bool> SoftDeleteRangeAsync(T[] entitys, SqlSugarScope Db = null)
        {
            BatchSetDelete(entitys);
            return Task.FromResult((Db ?? _dbContext.GetInstance()).Updateable(entitys).ExecuteCommandAsync().Result > 0);
        }
        #endregion

        #region ==Update==
        public virtual bool Update(T entity, SqlSugarScope Db = null)
        {
            SetModify(entity);
            return (Db ?? _dbContext.GetInstance()).Updateable(entity).ExecuteCommand() > 0;
        }

        public virtual Task<bool> UpdateAsync(T entity, SqlSugarScope Db = null)
        {
            SetModify(entity);
            return Task.FromResult((Db ?? _dbContext.GetInstance()).Updateable(entity).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool Update(Expression<Func<T, object>> columns, Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Updateable<T>().UpdateColumns(columns).Where(whereExpression).ExecuteCommand() > 0;
        }

        public virtual Task<bool> UpdateAsync(Expression<Func<T, object>> columns, Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return Task.FromResult((Db ?? _dbContext.GetInstance()).Updateable<T>().UpdateColumns(columns).Where(whereExpression).ExecuteCommandAsync().Result > 0);
        }

        public virtual bool UpdateRange(T[] entitys, SqlSugarScope Db = null)
        {
            BatchSetModify(entitys);
            return (Db ?? _dbContext.GetInstance()).Updateable(entitys).ExecuteCommand() > 0;
        }

        public virtual Task<bool> UpdateRangeAsync(T[] entitys, SqlSugarScope Db = null)
        {
            BatchSetModify(entitys);
            return Task.FromResult((Db ?? _dbContext.GetInstance()).Updateable(entitys).ExecuteCommandAsync().Result > 0);
        }
        #endregion

        #region ==Get==
        protected virtual T Get(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().Single(whereExpression);
        }

        protected virtual Task<T> GetAsync(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().SingleAsync(whereExpression);
        }

        public virtual List<T> GetList(SqlSugarScope Db = null)
        {
            return _dbContext.GetInstance().Queryable<T>().ToList();
        }

        public virtual Task<List<T>> GetListAsync(SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().ToListAsync();
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().Where(whereExpression).ToList();
        }

        public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().Where(whereExpression).ToListAsync();
        }

        public virtual int GetCount(SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().Count();
        }

        public virtual Task<int> GetCountAsync(SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().CountAsync();
        }

        public virtual int GetCount(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().Where(whereExpression).Count();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().Where(whereExpression).CountAsync();
        }
        #endregion

        #region ==First==
        public virtual T First(dynamic id, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().InSingle(id);
        }

        public virtual Task<T> FirstAsync(dynamic id, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().InSingleAsync(id);
        }

        public virtual T First(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().First(whereExpression);
        }

        public virtual Task<T> FirstAsync(Expression<Func<T, bool>> whereExpression, SqlSugarScope Db = null)
        {
            return (Db ?? _dbContext.GetInstance()).Queryable<T>().FirstAsync(whereExpression);
        }
        #endregion

        #region ==PageList==
        public List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page, SqlSugarScope Db = null)
        {
            int totalNumber = 0;
            List<T> result = (Db ?? _dbContext.GetInstance()).Queryable<T>().Where(whereExpression).ToPageList(page.PageIndex, page.PageSize, ref totalNumber);
            page.TotalCount = totalNumber;
            return result;
        }

        public List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc, SqlSugarScope Db = null)
        {
            int totalNumber = 0;
            List<T> result = (Db ?? _dbContext.GetInstance()).Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression)
                .ToPageList(page.PageIndex, page.PageSize, ref totalNumber);
            page.TotalCount = totalNumber;
            return result;
        }

        public List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page, SqlSugarScope Db = null)
        {
            int totalNumber = 0;
            List<T> result = (Db ?? _dbContext.GetInstance()).Queryable<T>().Where(conditionalList).ToPageList(page.PageIndex, page.PageSize, ref totalNumber);
            page.TotalCount = totalNumber;
            return result;
        }

        public List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc, SqlSugarScope Db = null)
        {
            int totalNumber = 0;
            List<T> result = (Db ?? _dbContext.GetInstance()).Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(conditionalList)
                .ToPageList(page.PageIndex, page.PageSize, ref totalNumber);
            page.TotalCount = totalNumber;
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
                var propertyType = item.PropertyType.Name;

                if (propertyType == "Guid" && item.GetValue(entity) != null && Guid.Parse(item.GetValue(entity)?.ToString()) != Guid.Empty)
                    continue;

                if (propertyType == "DateTime" && item.GetValue(entity) != null && DateTime.Parse(item.GetValue(entity)?.ToString()) != DateTime.MinValue)
                    continue;

                if (propertyType == "String" && item.GetValue(entity) != null && !string.IsNullOrEmpty(item.GetValue(entity)?.ToString()))
                    continue;

                switch (item.Name)
                {
                    case "Id":
                        if (propertyType == "Guid")
                            item.SetValue(entity, Guid.NewGuid());
                        else if (propertyType == "String")
                            item.SetValue(entity, Guid.NewGuid().ToString("N"));
                        break;

                    case "Creater":
                    case "Modifier":
                        item.SetValue(entity, _dbContext.DbOptions.UserKey == "UserId" ? _dbContext.LoginInfo?.UserId : _dbContext.LoginInfo?.UserCode);
                        break;

                    case "CreaterName":
                    case "ModifierName":
                        item.SetValue(entity, _dbContext.LoginInfo?.UserName);
                        break;

                    case "CreatedTime":
                    case "ModifiedTime":
                        item.SetValue(entity, dtNow);
                        break;

                    case "TenantId":
                        if (item.GetValue(entity) == null)
                            item.SetValue(entity, _dbContext.LoginInfo?.TenantId);
                        break;
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
                switch (item.Name)
                {
                    case "Modifier":
                        item.SetValue(entity, _dbContext.DbOptions.UserKey == "UserId" ? _dbContext.LoginInfo?.UserId : _dbContext.LoginInfo?.UserCode);
                        break;

                    case "ModifierName":
                        item.SetValue(entity, _dbContext.LoginInfo?.UserName);
                        break;

                    case "ModifiedTime":
                        item.SetValue(entity, dtNow);
                        break;
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
                switch (item.Name)
                {
                    case "Deleter":
                        item.SetValue(entity, _dbContext.DbOptions.UserKey == "UserId" ? _dbContext.LoginInfo?.UserId : _dbContext.LoginInfo?.UserCode);
                        break;

                    case "DeleterName":
                        item.SetValue(entity, _dbContext.LoginInfo?.UserName);
                        break;

                    case "DeletedTime":
                        item.SetValue(entity, dtNow);
                        break;

                    case "IsDel":
                        item.SetValue(entity, true);
                        break;
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
