 
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyModular;
using System.Linq;

namespace Demo.Admin.Application.ModulesService
{
    public class ModulesService : IModulesService
    {
        private readonly IMapper _mapper;
        private readonly IModulesRepository _repository;
        private readonly IList<IModuleDescriptor> _moduleDescriptors;
        public ModulesService(IMapper mapper, IModulesRepository repository, IList<IModuleDescriptor> moduleDescriptors)
        {
            _mapper = mapper;
            _repository = repository;
            _moduleDescriptors = moduleDescriptors;
        }

        public async Task<IResultModel> Query(ModulesQueryModel model)
        {
            var result = new QueryResultModel<ModulesEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(ModulesAddModel model)
        {
            var entity = _mapper.Map<ModulesEntity>(model);
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

            var model = _mapper.Map<ModulesUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(ModulesUpdateModel model)
        {
            var entity = await _repository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select()
        {
            var list = await _repository.GetListAsync(m => m.IsDel == false);
            var result = list.OrderBy(m => m.Code).Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Code
            });

            return ResultModel.Success(result);
        }


        public async Task<IResultModel> Sync()
        {
            var dtNow = DateTime.Now;
            var modules = new List<ModulesEntity>();
            var data = await _repository.GetListAsync(m => m.IsDel == false);

            foreach (var module in _moduleDescriptors)
            {
                if (data.Any(m => m.Code == module.Id))
                    continue;

                modules.Add(new ModulesEntity()
                {
                    Code = module.Id,
                    Name = module.Name,
                    Icon = module.Icon,
                    Version = module.Version,
                    Description = module.Description,
                    Sort=module.Sort,
                    Creater = "Sys",
                    CreaterName = "ϵͳ",
                    CreatedTime = dtNow,
                    Modifier = "Sys",
                    ModifierName = "ϵͳ",
                    ModifiedTime = dtNow
                });
            }

            var result = await _repository.InsertRangeAsync(modules);

            return ResultModel.Result(result);

        }

    }
}
