
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.TenantService;
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
    [Description("租户信息")]
    public class  TenantController : ModuleController
    {
        private readonly ITenantService _service;

        public  TenantController(ILoginInfo loginInfo,ITenantService EnterpriseService)
        {
            _service = EnterpriseService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]TenantQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(TenantAddModel model)
        {
            return _service.Add(model);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired]string  id)
        {
            return _service.Edit(id);
        }


        [HttpGet]
        [Description("信息")]
        [Common]
        public Task<IResultModel> Info()
        {
            return _service.Info();
        }

        [HttpPost]
        [Description("更新")]
        public Task<IResultModel> Update(TenantUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]string  id)
        {
            return _service.Delete(id);
        }

        [HttpPost]
        [Description("启用")]
        public Task<IResultModel> Enable([BindRequired]string  id)
        {
            return _service.Enable(id);
        }

        [HttpPost]
        [Description("禁用")]
        public Task<IResultModel> Disable([BindRequired]string  id)
        {
            return _service.Disable(id);
        }

        [HttpPost]
        [Description("移动")]
        public Task<IResultModel> Move(TenantMoveModel model)
        {
            return _service.Move(model);
        }

        [HttpGet]
        [Description("树形列表")]
        public Task<IResultModel> Tree([BindRequired] string tenantType)
        {
            return _service.GetTree(tenantType);
        }
    }
}

