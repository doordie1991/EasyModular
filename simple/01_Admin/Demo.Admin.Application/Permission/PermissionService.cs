
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using Demo.Admin.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _repository;
        private readonly MvcHelper _mvcHelper;
        private readonly DbContext _dbContext;
        private readonly ILogger _logger;
        public PermissionService(IMapper mapper
            , IPermissionRepository repository
            , ApplicationPartManager partManager
            , DbContext dbContext
            , ILogger<PermissionService> logger
            )
        {
            _mapper = mapper;
            _repository = repository;
            _mvcHelper = new MvcHelper(partManager);
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IResultModel> Query(PermissionQueryModel model)
        {
            var result = new QueryResultModel<PermissionEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IList<PermissionEntity>> QueryByUserId(string userId)
        {
            var result = await _repository.QueryByUserId(userId);
            return result;
        }

        public async Task<IResultModel> Add(PermissionAddModel model)
        {
            if (await _repository.ExistsAsync(m => m.PermissionCode == model.PermissionCode && m.IsDel == false))
                return ResultModel.HasExists;

            var entity = _mapper.Map<PermissionEntity>(model);
            var result = await _repository.InsertAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(string id)
        {
            var entity = await _repository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var result = await _repository.SoftDeleteAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _repository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<PermissionUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(PermissionUpdateModel model)
        {
            var entity = await _repository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _repository.ExistsAsync(m => m.PermissionCode == model.PermissionCode && m.Id != entity.Id && m.IsDel == false))
                return ResultModel.HasExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Sync()
        {
            var permissionEntities = GetActions();
            try
            {
                _dbContext.Db.BeginTran();

                await _repository.DeleteAsync(m => m.Source == PermissionSource.Auto, _dbContext.Db);
                await _repository.InsertRangeAsync(permissionEntities, _dbContext.Db);

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        private List<PermissionEntity> GetActions()
        {
            var list = new List<PermissionEntity>();
            var actions = _mvcHelper.GetAllAction();

            foreach (var action in actions)
            {
                //排除匿名接口和通用接口
                if (action.MethodInfo.CustomAttributes.Any(m => m.AttributeType == typeof(AllowAnonymousAttribute) || m.AttributeType == typeof(CommonAttribute)))
                    continue;

                var p = new PermissionEntity
                {
                    Area = action.Controller.Area,
                    Controller = action.Controller.Name,
                    Action = action.Name,
                    PermissionName = $"{action.Controller.Description ?? action.Controller.Name}_{action.Description ?? action.Name}",
                    Source = PermissionSource.Auto
                };

                var httpMethodAttr = action.MethodInfo.CustomAttributes.FirstOrDefault(m => m.AttributeType.Name.StartsWith("Http"));

                if (httpMethodAttr != null)
                {
                    var httpMethodName = httpMethodAttr.AttributeType.Name.Replace("Http", "").Replace("Attribute", "").ToUpper();
                    p.HttpMethod = httpMethodName;
                    p.PermissionCode = $"{p.Area}_{p.Controller}_{p.Action}_{httpMethodName}".ToLower();

                    list.Add(p);
                }
            }

            return list;
        }

    }
}
