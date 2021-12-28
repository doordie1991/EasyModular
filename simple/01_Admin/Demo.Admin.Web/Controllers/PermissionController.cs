
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.PermissionService;
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
    [Description("权限")]
    public class  PermissionController : ModuleController
    {
        private readonly IPermissionService _service;

        public  PermissionController(ILoginInfo loginInfo,IPermissionService PermissionService)
        {
            _service = PermissionService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]PermissionQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(PermissionAddModel model)
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
        public Task<IResultModel> Update(PermissionUpdateModel model)
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
        [Description("同步")]
        public Task<IResultModel> Sync()
        {
            return _service.Sync();
        }
    }
}

