
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyModular;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Reflection;
using SqlSugar;
using Demo.Admin.Infrastructure;

namespace Demo.Admin.Application.ResourceService
{
    public class ResourceService : IResourceService
    {
        private readonly IMapper _mapper;
        private readonly DbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IResourceRepository _resourceRepository;
        private readonly IResourceDetailRepository _resourceDetailRepository;
        private readonly IResourceFilterRepository _resourceFilterRepository;
        private readonly IResourceWhitelistRepository _resourceWhitelistRepository;
        private readonly IList<IModuleDescriptor> _moduleDescriptors;

        public ResourceService(IMapper mapper
            , DbContext dbContext
            , ILogger<ResourceService> logger
            , IResourceRepository resourceRepository
            , IResourceDetailRepository resourceDetailRepository
            , IResourceFilterRepository resourceFilterRepository
            , IResourceWhitelistRepository resourceWhitelistRepository
            , IList<IModuleDescriptor> moduleDescriptors)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
            _resourceRepository = resourceRepository;
            _resourceDetailRepository = resourceDetailRepository;
            _resourceFilterRepository = resourceFilterRepository;
            _resourceWhitelistRepository = resourceWhitelistRepository;
            _moduleDescriptors = moduleDescriptors;
        }

        public async Task<IResultModel> Query(ResourceQueryModel model)
        {
            var result = new QueryResultModel<ResourceEntity>
            {
                Rows = await _resourceRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(ResourceAddModel model)
        {
            try
            {
                _dbContext.Db.BeginTran();

                var entity = _mapper.Map<ResourceEntity>(model);
                var result = await _resourceRepository.InsertAsync(entity, _dbContext.Db);

                var details = model.Details.Where(m => !string.IsNullOrEmpty(m.FieldName)).ToList();
                if (result && details.Any())
                {
                    details.ForEach(m => { m.ResourceId = entity.Id; });
                    await _resourceDetailRepository.InsertRangeAsync(_mapper.Map<List<ResourceDetailEntity>>(details), _dbContext.Db);
                }

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Delete(string id)
        {
            try
            {
                var entity = await _resourceRepository.FirstAsync(id);
                if (entity == null)
                    return ResultModel.NotExists;

                _dbContext.Db.BeginTran();

                await _resourceRepository.SoftDeleteAsync(entity);
                await _resourceDetailRepository.DeleteAsync(m => m.ResourceId == id);
                await _resourceFilterRepository.DeleteAsync(m => m.ResourceId == id);
                await _resourceWhitelistRepository.DeleteAsync(m => m.ResourceId == id);

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _resourceRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<ResourceUpdateModel>(entity);

            model.Details = await _resourceDetailRepository.GetListAsync(m => m.ResourceId == id & m.IsDel == false);
            model.Filters = await _resourceFilterRepository.GetListAsync(m => m.ResourceId == id & m.IsDel == false);
            model.Whitelists = await _resourceWhitelistRepository.GetListAsync(m => m.ResourceId == id & m.IsDel == false);

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(ResourceUpdateModel model)
        {
            var entity = await _resourceRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            try
            {
                _dbContext.Db.BeginTran();

                var result = await _resourceRepository.UpdateAsync(entity, _dbContext.Db);

                if (result)
                {
                    await _resourceDetailRepository.DeleteAsync(m => m.ResourceId == entity.Id);

                    var details = model.Details.Where(m => !string.IsNullOrEmpty(m.FieldName)).ToList();
                    details.ForEach(m => { m.ResourceId = entity.Id; });

                    await _resourceDetailRepository.InsertRangeAsync(_mapper.Map<List<ResourceDetailEntity>>(details), _dbContext.Db);
                }

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Permissions(ResourceUpdateModel model)
        {
            var entity = await _resourceRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            try
            {
                _dbContext.Db.BeginTran();

                await _resourceRepository.UpdateAsync(entity, _dbContext.Db);

                //过滤
                await _resourceFilterRepository.DeleteAsync(m => m.ResourceId == entity.Id, _dbContext.Db);
                await _resourceFilterRepository.InsertRangeAsync(model.Filters, _dbContext.Db);

                //白名单
                await _resourceWhitelistRepository.DeleteAsync(m => m.ResourceId == entity.Id, _dbContext.Db);
                await _resourceWhitelistRepository.InsertRangeAsync(model.Whitelists, _dbContext.Db);

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Sync()
        {
            try
            {
                var dtNow = DateTime.Now;
                var data = await _resourceRepository.GetListAsync(m => m.IsDel == false);
                var resources = new List<ResourceEntity>();
                var resourcesDetails = new List<ResourceDetailEntity>();

                foreach (var module in _moduleDescriptors)
                {
                    var entities = module.AssemblyDescriptor.Domain.GetTypes().Where(m => m.Name.EndsWith("Entity"));
                    foreach (var entity in entities)
                    {
                        var classInfo = entity.GetCustomAttribute<SugarTable>();
                        if (classInfo == null || string.IsNullOrEmpty(classInfo.TableDescription) || data.Any(m => m.Code == classInfo.TableName))
                            continue;

                        var resourceId = Guid.NewGuid().ToString("N");
                        resources.Add(new ResourceEntity()
                        {
                            Id = resourceId,
                            Module = module.Id,
                            Code = classInfo.TableName,
                            Name = classInfo.TableDescription,
                            Source = DataSource.Auto,
                            Creater = "Sys",
                            CreaterName = "系统",
                            CreatedTime = dtNow,
                            Modifier = "Sys",
                            ModifierName = "系统",
                            ModifiedTime = dtNow
                        });

                        var pros = entity.GetProperties();
                        foreach (var property in pros)
                        {
                            resourcesDetails.Add(new ResourceDetailEntity()
                            {
                                ResourceId = resourceId,
                                FieldName = property.Name,
                                FieldType = property.PropertyType.Name
                            });
                        }
                    }
                }

                _dbContext.Db.BeginTran();

                await _resourceRepository.InsertRangeAsync(resources, _dbContext.Db);
                await _resourceDetailRepository.InsertRangeAsync(resourcesDetails, _dbContext.Db);

                _dbContext.Db.CommitTran();

            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                _logger.LogError($"同步资源失败:{ex.Message}", ex);
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

    }
}
