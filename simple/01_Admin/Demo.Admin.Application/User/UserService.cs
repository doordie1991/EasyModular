
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.Admin.Infrastructure;
using System.Linq;
using EasyModular.Auth;

namespace Demo.Admin.Application.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DbContext _dbContext;
        private readonly ILoginInfo _loginInfo;
        private readonly IUserRepository _userRepository;
        private readonly IRoleUserRepository _roleUserRepository;
        private readonly IOrganizeRepository _organizeRepository;
        private readonly IUserLatestSelectRepository _userLatestSelectRepository;
        private readonly IPasswordHandler _passwordHandler;
        public UserService(IMapper mapper
            , DbContext dbContext
            , ILoginInfo loginInfo
            , IUserRepository userRepository
            , IRoleUserRepository roleUserRepository
            , IOrganizeRepository organizeRepository
            , IUserLatestSelectRepository userLatestSelectRepository
            , IPasswordHandler passwordHandler)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _loginInfo = loginInfo;
            _userRepository = userRepository;
            _roleUserRepository = roleUserRepository;
            _organizeRepository = organizeRepository;
            _userLatestSelectRepository = userLatestSelectRepository;
            _passwordHandler = passwordHandler;
        }

        public async Task<IResultModel> Query(UserQueryModel model)
        {
            var data = await _userRepository.Query(model);
            var result = new QueryResultModel<UserViewModel>
            {
                Rows = _mapper.Map<List<UserViewModel>>(data),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> QueryByUserCodes(List<string> userCodes)
        {
            var result = await _userRepository.QueryByUserCodes(userCodes);

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> QueryByUserIds(List<string> userIds)
        {
            var result = await _userRepository.QueryByUserIds(userIds);

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> QueryLatestSelect(UserQueryModel model)
        {
            var result = new QueryResultModel<UserLatestSelectEntity>
            {
                Rows = await _userRepository.QueryLatestSelect(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> QueryBySameOrg(UserQueryModel model)
        {
            var result = new QueryResultModel<UserEntity>
            {
                Rows = await _userRepository.QueryBySameOrg(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(UserAddModel model)
        {
            if (await _userRepository.ExistsAsync(m => m.UserCode == model.UserCode && m.IsDel == false))
                return ResultModel.HasExists;

            try
            {
                model.Password = _passwordHandler.Encrypt(model.UserCode, model.Password);
                var entity = _mapper.Map<UserEntity>(model);
                await _userRepository.InsertAsync(entity,_dbContext.Db);

                var roles = new List<RoleUserEntity>();
                foreach (var roleId in model.RoleIds)
                {
                    roles.Add(new RoleUserEntity()
                    {
                        RoleId= roleId,
                        UserId=entity.Id
                    });
                }

                if (roles.Any())
                    await _roleUserRepository.InsertRangeAsync(roles);

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
            var entity = await _userRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var result = await _userRepository.SoftDeleteAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _userRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<UserUpdateModel>(entity);
            model.RoleIds = (await _roleUserRepository.GetListAsync(m => m.UserId == entity.Id && m.IsDel == false)).Select(m => m.RoleId).ToList();

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(UserUpdateModel model)
        {
            var entity = await _userRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _userRepository.ExistsAsync(m => m.UserCode == model.UserCode && m.Id != entity.Id && m.IsDel == false))
                return ResultModel.HasExists;

            try
            {
                _mapper.Map(model, entity);
                entity.Password = _passwordHandler.Encrypt(model.UserCode, model.Password);
                await _userRepository.UpdateAsync(entity, _dbContext.Db);

                //先清除原来角色用户，再重新添加
                await _roleUserRepository.DeleteAsync(m => m.UserId == entity.Id);

                var roles = new List<RoleUserEntity>();
                foreach (var roleId in model.RoleIds)
                {
                    roles.Add(new RoleUserEntity()
                    {
                        RoleId = roleId,
                        UserId = entity.Id
                    });
                }

                if (roles.Any())
                    await _roleUserRepository.InsertRangeAsync(roles);

                _dbContext.Db.CommitTran();

            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Enable(string id)
        {
            var entity = await _userRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            entity.Status = UserStatus.Enabled;

            var result = await _userRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Disable(string id)
        {
            var entity = await _userRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            entity.Status = UserStatus.Disabled;

            var result = await _userRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> UpdatePassword(UpdatePasswordModel model)
        {
            var user = await _userRepository.FirstAsync(m => m.Id == _loginInfo.UserId && m.IsDel == false && m.Status == UserStatus.Enabled);

            if (user == null)
                return ResultModel.Failed("用户不存在");

            var oldPassword = _passwordHandler.Encrypt(user.UserCode, model.OldPassword);
            if (!user.Password.Equals(oldPassword))
                return ResultModel.Failed("原密码错误");

            var newPassword = _passwordHandler.Encrypt(user.UserCode, model.NewPassword);
            var result = await _userRepository.UpdatePassword(_loginInfo.UserId, newPassword);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select(string keywords)
        {
            var list = await _userRepository.GetTop(keywords, 10); 
            var result = list.Select(m => new OptionResultModel
            {
                Label = m.UserName,
                Value = m.UserCode,
                Data = new
                {
                    m.Id,
                    m.UserCode,
                    m.UserName
                }
            }).ToList();

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> SaveLatestSelect(List<string> ids)
        {
            if (ids == null || !ids.Any())
                return ResultModel.Failed("未选择用户");

            try
            {
                _dbContext.Db.BeginTran();

                foreach (var relationId in ids)
                {
                    var entity = await _userLatestSelectRepository.FirstAsync(m => m.UserId == _loginInfo.UserId && m.RelationId == relationId && m.IsDel == false, _dbContext.Db);
                    if (entity == null)
                    {
                        entity = new UserLatestSelectEntity
                        {
                            UserId = _loginInfo.UserId,
                            RelationId = relationId,
                            Timestamp = DateTime.Now.ToTimestamp()
                        };

                        await _userLatestSelectRepository.InsertAsync(entity, _dbContext.Db);
                    }
                    else
                    {
                        entity.Timestamp = DateTime.Now.ToTimestamp();
                        await _userLatestSelectRepository.UpdateAsync(entity, _dbContext.Db);
                    }
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

        public async Task<IResultModel> Move(UserMoveModel model)
        {
            var entity = await _userRepository.FirstAsync(model.UserId);
            if (entity == null)
                return ResultModel.NotExists;

            if (model.SourceId == model.TargetId)
                return ResultModel.Failed("不能移动至原位置");

            entity.OrganizeId = model.TargetId;

            var result = await _userRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> GetTree()
        {
            var list = new List<TreeResultModel<string, UserTreeResultModel>>();

            var allOrg = await _organizeRepository.GetListAsync(m => m.TenantId == _loginInfo.TenantId && m.IsDel == false);
            var allUser = await _userRepository.GetListAsync(m => m.TenantId == _loginInfo.TenantId && m.IsDel == false);
            list = ResolveTree(allOrg, allUser, Guid.Empty.ToString("N"));

            return ResultModel.Success(list);
        }

        private List<TreeResultModel<string, UserTreeResultModel>> ResolveTree(IList<OrganizeEntity> allOrg, IList<UserEntity> allUser, string parentId)
        {
            return allOrg.Where(m => m.ParentId == parentId).OrderBy(m => m.OrganizeCode)
                .Select(m =>
                {
                    var node = new TreeResultModel<string, UserTreeResultModel>
                    {
                        Id = m.Id,
                        Label = m.OrganizeName,
                        Item = new UserTreeResultModel
                        {
                            Type = 0,
                            SourceId = m.OrganizeCode,
                            Name = m.OrganizeName
                        },
                        Children = new List<TreeResultModel<string, UserTreeResultModel>>()
                    };

                    node.Children.AddRange(ResolveTree(allOrg, allUser, m.Id));

                    var users = allUser.Where(t => t.OrganizeId == m.Id);

                    if(!users.Any())
                        return node;

                    foreach (var user in users)
                    {
                        node.Children.Add(new TreeResultModel<string, UserTreeResultModel>
                        {
                            Id = user.Id,
                            Label = user.UserName,
                            Item = new UserTreeResultModel
                            {
                                Type = 1,
                                SourceId = user.Id.ToString(),
                                Code=user.UserCode,
                                Name = user.UserName,
                                Sex = user.Sex,
                                JobName=user.JobName
                            }
                        });
                    }

                    return node;
                }).ToList();
        }

    }
}
