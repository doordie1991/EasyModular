 
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.ModulesService;
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
    [Description("模块")]
    public class  ModulesController : ModuleController
    {
        private readonly IModulesService _service;

        public  ModulesController(ILoginInfo loginInfo,IModulesService ModulesService)
        {
            _service = ModulesService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]ModulesQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(ModulesAddModel model)
        {
            return _service.Add(model);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired]string id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("更新")]
        public Task<IResultModel> Update(ModulesUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]string id)
        {
            return _service.Delete(id);
        }

        [HttpGet]
        [Common]
        [Description("下拉列表")]
        public Task<IResultModel> Select()
        {
            return _service.Select();
        }
    }
}

