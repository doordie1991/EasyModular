
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.DictionaryGroupService;
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
    [Description("字典分组")]
    public class  DictionaryGroupController : ModuleController
    {
        private readonly IDictionaryGroupService _service;

        public  DictionaryGroupController(ILoginInfo loginInfo,IDictionaryGroupService DictionaryGroupService)
        {
            _service = DictionaryGroupService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]DictionaryGroupQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(DictionaryGroupAddModel model)
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
        public Task<IResultModel> Update(DictionaryGroupUpdateModel model)
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
        [Common]
        [Description("下拉列表")]
        public Task<IResultModel> Select()
        {
            return _service.Select();
        }
    }
}

