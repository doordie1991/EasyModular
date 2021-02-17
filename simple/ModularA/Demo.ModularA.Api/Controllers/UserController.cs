using EasyModular.Auth;
using EasyModular.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Demo.ModularA.Application.User;
using Demo.ModularA.Application.User.Models;
using Demo.ModularA.Domain.User.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Demo.ModularA.Api.Controllers
{

    [Description("用户")]
    public class UserController : ModuleController
    {
        private readonly IUserService _service;

        public UserController(IUserService UserService)
        {
            _service = UserService;
        }

        [HttpGet]
        [Description("查询")]
        [AllowAnonymous]
        public Task<IResultModel> Query([FromQuery]UserQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        [AllowAnonymous]
        public Task<IResultModel> Add(UserAddModel model)
        {
            return _service.Add(model);
        }

        [HttpGet]
        [Description("编辑")]
        [AllowAnonymous]
        public Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("更新")]
        [AllowAnonymous]
        public Task<IResultModel> Update(UserUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpDelete]
        [Description("删除")]
        [AllowAnonymous]
        public Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return _service.Delete(id);
        }
    }
}
