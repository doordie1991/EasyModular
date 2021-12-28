
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.DictionaryService;
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
    [Description("字典")]
    public class  DictionaryController : ModuleController
    {
        private readonly IDictionaryService _service;

        public  DictionaryController(ILoginInfo loginInfo,IDictionaryService DictionaryService)
        {
            _service = DictionaryService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]DictionaryQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(DictionaryAddModel model)
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
        public Task<IResultModel> Update(DictionaryUpdateModel model)
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
        [Description("下拉列表")]
        [Common]
        public Task<IResultModel> Select(string group, string code)
        {
            return _service.Select(group, code);
        }

        [HttpGet]
        [Description("树列表")]
        [Common]
        public Task<IResultModel> Tree(string group, string code)
        {
            return _service.Tree(group, code);
        }
    }
}

