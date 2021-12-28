
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Demo.Admin.Application.MenuButtonService
{
    public class MenuButtonService : IMenuButtonService
    {
        private readonly IMapper _mapper;
        private readonly IMenuButtonRepository _repository;
        public MenuButtonService(IMapper mapper, IMenuButtonRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(MenuButtonQueryModel model)
        {
            var result = new QueryResultModel<MenuButtonEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> QueryByMenuId(string menuId)
        {
            var result = await _repository.GetListAsync(m => m.MenuId == menuId && m.IsDel == false);
            return ResultModel.Success(result.OrderBy(o => o.Sort));
        }

        public async Task<IResultModel> Add(MenuButtonAddModel model)
        {
            var entity = _mapper.Map<MenuButtonEntity>(model);
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

            var model = _mapper.Map<MenuButtonUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(MenuButtonUpdateModel model)
        {
            var entity = await _repository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

    }
}
