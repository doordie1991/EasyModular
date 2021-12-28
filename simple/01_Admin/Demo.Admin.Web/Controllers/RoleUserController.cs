
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.RoleUserService;
using Demo.Admin.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Web
{
    [Description("角色用户")]
    public class  RoleUserController : ModuleController
    {
        private readonly IRoleUserService _service;

        public  RoleUserController(ILoginInfo loginInfo,IRoleUserService RoleUserService)
        {
            _service = RoleUserService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]RoleUserQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(RoleUserAddModel model)
        {
            return _service.Add(model);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired]string  id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("更新")]
        public Task<IResultModel> Update(RoleUserUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]string  id)
        {
            return _service.Delete(id);
        }

        [HttpGet]
        [Description("下拉")]
        [Common]
        public Task<IResultModel> Select([BindRequired] string roleCode)
        {
            return _service.Select(roleCode);
        }
    }
}

