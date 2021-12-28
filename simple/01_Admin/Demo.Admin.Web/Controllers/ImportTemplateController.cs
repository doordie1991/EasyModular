
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.ImportTemplateService;
using Demo.Admin.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.Admin.Web
{
    [Description("导入模板")]
    public class  ImportTemplateController : ModuleController
    {
        private readonly IImportTemplateService _service;

        public  ImportTemplateController(ILoginInfo loginInfo,IImportTemplateService ImportTemplateService)
        {
            _service = ImportTemplateService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]ImportTemplateQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(ImportTemplateAddModel model)
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
        public Task<IResultModel> Update(ImportTemplateUpdateModel model)
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
        [Description("根据编码查询")]
        [Common]
        public Task<IResultModel> QueryByCode(string moduleCode, string templateCode)
        {
            return _service.QueryByCode(moduleCode, templateCode);
        }
    }
}

