
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.RoleService;
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
    [Description("角色")]
    public class  RoleController : ModuleController
    {
        private readonly IRoleService _service;

        public  RoleController(ILoginInfo loginInfo,IRoleService RoleService)
        {
            _service = RoleService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]RoleQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(RoleAddModel model)
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
        public Task<IResultModel> Update(RoleUpdateModel model)
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
        [Description("角色树形列表")]
        public Task<IResultModel> Tree()
        {
            return _service.Tree();
        }


        [HttpGet]
        [Description("获取角色的关联菜单树形列表")]
        [Common]
        public Task<IResultModel> MenuTree([BindRequired] string  id)
        {
            return _service.MenuTree(id);
        }

        [HttpPost]
        [Description("绑定菜单权限")]
        public Task<IResultModel> BindMenuPermission(RoleMenuPermissionBindModel model)
        {
            return _service.BindMenuPermission(model);
        }
    }
}

