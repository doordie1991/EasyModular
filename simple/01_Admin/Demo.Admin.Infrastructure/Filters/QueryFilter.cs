using Demo.Admin.Domain;
using EasyModular.Auth;
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class QueryFilter : IQueryFilter
    {
        private readonly ILoginInfo _loginInfo;
        private readonly DbContext _dbContext;

        public QueryFilter(ILoginInfo loginInfo, DbContext dbContext)
        {
            _loginInfo = loginInfo;
            _dbContext = dbContext;
        }

        public async Task<List<IConditionalModel>> GetConditions<TEntity>()
        {
            var conModels = new List<IConditionalModel>();

            conModels.Add(new ConditionalModel()
            {
                FieldName = "IsDel",
                ConditionalType = ConditionalType.Equal,
                FieldValue = "0",
                FieldValueConvertFunc = it => Convert.ToInt32(it)
            });

            var tableName = GetTableName<TEntity>();
            var resource =await GetResource(tableName);

            //如果是超级管理则不受资源权限控制
            if (_loginInfo.TenantType == TenantType.System && _loginInfo.RoleCodes.Contains(RoleKey.ROLE_SUPER_CODE))
                return conModels;

            if (resource.IsolateType == IsolateType.Tenant)
                conModels.Add(IsolateByTenant());

            if (resource.IsolateType == IsolateType.Trade)
                conModels.Add(IsolateByTrade());

            //如果用户属于白名单则不受设置的资源过滤条件控制
            if (await IsInWhitelist(resource.Id))
                return conModels;

            var resourceFilters = await GetResourceFilter(resource.Id);
            conModels.AddRange(resourceFilters);

            return conModels;
        }

        public async Task<List<IConditionalModel>> GetConditions<TEntity, TQuery>(TQuery query)
        {
            var conModels =await GetConditions<TEntity>();
            foreach (var property in typeof(TQuery).GetProperties())
            {
                var condition = property.GetCustomAttribute<ConditionAttribute>();
                if (condition == null)
                    continue;

                var propertyVal = property.GetValue(query);
                if (propertyVal == null || IsMinTime(propertyVal))
                    continue;

                conModels.Add(new ConditionalModel()
                {
                    FieldName = string.IsNullOrEmpty(condition.FieldName) ? property.Name : condition.FieldName,
                    ConditionalType = condition.ConditionalType,
                    FieldValue = propertyVal.ToString(),
                    FieldValueConvertFunc = FieldValueConvert(propertyVal, condition.ConvertType)
                });
            }

            return conModels;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        private string GetTableName<T>()
        {
            var attr = typeof(T).GetCustomAttribute<SugarTable>();
            return attr?.TableName;
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        private async Task<ResourceEntity> GetResource(string tableName)
        {
            var data = await _dbContext.Db.Queryable<ResourceEntity>().Where(m => m.Code == tableName && m.IsDel == false).FirstAsync();
            return data;
        }

        /// <summary>
        /// 获取资源过滤条件
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        private async Task<List<ConditionalModel>> GetResourceFilter(string resourceId)
        {
            var conModels = new List<ConditionalModel>();

            var data = await _dbContext.Db.Queryable<ResourceFilterEntity>().Where(m => m.ResourceId == resourceId && m.IsDel == false).ToListAsync();
            foreach (var item in data)
            {
                conModels.Add(new ConditionalModel()
                {
                    FieldName = item.FieldName,
                    ConditionalType = item.ConditionalType,
                    FieldValue = _loginInfo.GetType().GetProperty(item.FieldName).GetValue(_loginInfo).ToString()
                });
            }

            return conModels;
        }

        /// <summary>
        /// 获取资源白名单
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        private async Task<List<ResourceWhitelistEntity>> GetResourceWhitelist(string resourceId)
        {
            var data = await _dbContext.Db.Queryable<ResourceWhitelistEntity>().Where(m => m.ResourceId == resourceId && m.IsDel == false).ToListAsync();
            return data;
        }

        /// <summary>
        /// 按租户隔离
        /// </summary>
        /// <returns></returns>
        private ConditionalModel IsolateByTenant()
        {
            if (_loginInfo.TenantType == TenantType.Normal)
            {
                return new ConditionalModel()
                {
                    FieldName = "TenantId",
                    ConditionalType = ConditionalType.Equal,
                    FieldValue = _loginInfo.TenantId
                };
            }
            else
            {
                var tenantIds = string.Join(',', this.GetRelateTenantIds(_loginInfo.TenantId));
                return new ConditionalModel()
                {
                    FieldName = "TenantId",
                    ConditionalType = ConditionalType.In,
                    FieldValue = tenantIds
                };
            }

        }

        /// <summary>
        /// 按行业隔离
        /// </summary>
        /// <returns></returns>
        private ConditionalCollections IsolateByTrade()
        {
            var conditions = new List<KeyValuePair<WhereType, ConditionalModel>>()
            {
                new  KeyValuePair<WhereType, ConditionalModel>( WhereType.And ,new ConditionalModel()
                     {
                       FieldName = "Trade",
                       ConditionalType = ConditionalType.Equal,
                       FieldValue =  "all"
                     }),
                new  KeyValuePair<WhereType, ConditionalModel>( WhereType.Or ,new ConditionalModel()
                     {
                       FieldName = "Trade",
                       ConditionalType = ConditionalType.Equal,
                       FieldValue =  _loginInfo.Trade
                     }),
                new  KeyValuePair<WhereType, ConditionalModel>( WhereType.Or ,new ConditionalModel()
                     {
                       FieldName = "TenantId",
                       ConditionalType = ConditionalType.Equal,
                       FieldValue =  _loginInfo.TenantId
                     })
            };

            var collections = new ConditionalCollections()
            {
                ConditionalList = conditions
            };

            return collections;
        }

        /// <summary>
        /// 是否在白名单
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        private async Task<bool> IsInWhitelist(string resourceId)
        {
            var whitelist = await GetResourceWhitelist(resourceId);
            if (whitelist.Any(m => m.Value == _loginInfo.UserId) || whitelist.Any(m => _loginInfo.RoleCodes.Contains(m.Value)))
                return true;

            return false;
        }

        /// <summary>
        /// 是否最小时间
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private bool IsMinTime(object val)
        {
            if (val == null)
                return false;

            if (val.GetType().Name != "DateTime")
                return false;

            if (DateTime.Parse(val.ToString()) == DateTime.MinValue)
                return true;

            return false;
        }

        /// <summary>
        /// 字段值类型转换
        /// </summary>
        /// <param name="propertyVal"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private Func<string, object> FieldValueConvert(object propertyVal, string func)
        {
            switch (func)
            {
                case EasyModular.SqlSugar.ConvertType.ToInt:
                    return m => Convert.ToInt32(propertyVal);

                case EasyModular.SqlSugar.ConvertType.ToBoolean:
                    return m => Convert.ToBoolean(propertyVal);

                case EasyModular.SqlSugar.ConvertType.ToDateTime:
                    return m => Convert.ToDateTime(propertyVal);

                case EasyModular.SqlSugar.ConvertType.ToEnum:
                    return m => Convert.ToInt32(propertyVal);

                default:
                    return m => Convert.ToString(propertyVal);
            }
        }

        /// <summary>
        /// 获取关联租户
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IQueryable<string> GetRelateTenantIds(string parentId)
        {
            var result = new List<string>();

            var data = _dbContext.Db.Queryable<TenantTreeEntity>()
                             .Where(m => m.IsDel == false)
                             .ToChildList(m => m.ParentId, parentId)
                             .Select(m => m.Id);

            result.AddRange(data);
            return result.AsQueryable();
        }


    }
}
