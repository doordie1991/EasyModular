
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.PackageService;
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
    [Description("套餐")]
    public class PackageController : ModuleController
    {
        private readonly IPackageService _service;

        public PackageController(ILoginInfo loginInfo, IPackageService PackageService)
        {
            _service = PackageService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] PackageQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(PackageAddModel model)
        {
            return _service.Add(model);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired] string  id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("更新")]
        public Task<IResultModel> Update(PackageUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired] string  id)
        {
            return _service.Delete(id);
        }

        [HttpPost]
        [Description("启用")]
        public Task<IResultModel> Enable([BindRequired] string  id)
        {
            return _service.Enable(id);
        }

        [HttpPost]
        [Description("禁用")]
        public Task<IResultModel> Disable([BindRequired] string  id)
        {
            return _service.Disable(id);
        }

        [HttpGet]
        [Common]
        [Description("下拉列表")]
        public Task<IResultModel> Select([BindRequired] string tenantType)
        {
            return _service.Select(tenantType);
        }
    }
}

