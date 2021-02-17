using Demo.ModularA.Domain.Organization;
using Demo.ModularA.Domain.User;
using Demo.ModularA.Infrastructure.Repositories;
using EasyModular.SqlSugar;
using EasyModular.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ModularA.Application.Test
{
    public class TestService : ITestService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationRepository _organizationRepositor;
        private readonly DbContext _dbContext;
        public TestService(IUserRepository userRepository, IOrganizationRepository organizationRepositor, DbContext dbContext)
        {
            _userRepository = userRepository;
            _organizationRepositor = organizationRepositor;
            _dbContext = dbContext;
        }

        public async Task<IResultModel> TestTran()
        {
            try
            {
                _dbContext.Db.BeginTran();
             
                var userEntity = new UserEntity()
                {
                    UserCode = "11",
                    UserName = "22",
                    Password="33"
                };
                var res1 = await _userRepository.InsertAsync(userEntity, false);

                var organizationEntity = new OrganizationEntity()
                {
                    OrganizeCode = "33",
                    OrganizeName = "44"
                };

                var res2 = await _organizationRepositor.InsertAsync(organizationEntity, false);

                if (res1 && res2)
                    _dbContext.Db.CommitTran();

            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

    }
}
