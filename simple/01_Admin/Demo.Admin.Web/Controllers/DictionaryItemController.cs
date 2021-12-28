
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.DictionaryItemService;
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
    [Description("字典项")]
    public class  DictionaryItemController : ModuleController
    {
        private readonly IDictionaryItemService _service;

        public  DictionaryItemController(ILoginInfo loginInfo,IDictionaryItemService DictionaryItemService)
        {
            _service = DictionaryItemService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]DictionaryItemQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(DictionaryItemAddModel model)
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
        public Task<IResultModel> Update(DictionaryItemUpdateModel model)
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
        [Description("导入")]
        [Common]
        public Task<IResultModel> Import([FromForm] DictionaryItemAddModel model,IFormFile file)
        {
            return _service.Import(model,file);
        }
    }
}

