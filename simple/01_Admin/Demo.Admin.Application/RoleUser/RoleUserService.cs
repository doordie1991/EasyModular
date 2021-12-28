 
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using EasyModular;
using EasyModular.Auth;

namespace Demo.Admin.Application.RoleUserService
{
    public class RoleUserService : IRoleUserService
    {
        private readonly IMapper _mapper;
        private readonly ILoginInfo _loginInfo;
        private readonly IRoleUserRepository _roleUserRepository;
        private readonly IRoleRepository _roleRepository;
        public RoleUserService(IMapper mapper, ILoginInfo loginInfo, IRoleUserRepository roleUserRepository, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _loginInfo = loginInfo;
            _roleUserRepository = roleUserRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IResultModel> Query(RoleUserQueryModel model)
        {
            var result = new QueryResultModel<RoleUserEntity>
            {
                Rows = await _roleUserRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(RoleUserAddModel model)
        {
            if (!await _roleRepository.ExistsAsync(m => m.Id == model.RoleId))
                return ResultModel.Failed("角色不存在");

            if (!model.UserIds.Any())
                return ResultModel.Failed("请选择用户");

            var entities = new List<RoleUserEntity>();

            foreach (var userId in model.UserIds)
            {
                var isExist = await _roleUserRepository.ExistsAsync(m => m.RoleId == model.RoleId && m.UserId == userId && m.IsDel == false);
                if (isExist)
                    continue;

                entities.Add(new RoleUserEntity()
                {
                    RoleId = model.RoleId,
                    UserId = userId
                });
            }

            if (!entities.Any())
                return ResultModel.Success();

            var result = await _roleUserRepository.InsertRangeAsync(entities);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(string id)
        {
            var entity = await _roleUserRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var result = await _roleUserRepository.SoftDeleteAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _roleUserRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<RoleUserUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(RoleUserUpdateModel model)
        {
            var entity = await _roleUserRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _roleUserRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select(string roleCode)
        {

            var list = await _roleUserRepository.QueryByRoleCode(roleCode);
            var result = list.Select(m => new OptionResultModel
            {
                Label = m.UserName,
                Value = m.UserId,
                Data = new
                {
                    m.UserId,
                    m.UserCode,
                    m.UserName
                }
            }).ToList();

            return ResultModel.Success(result);
        }

    }
}
