
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.ReleaseLogService;
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
    [Description("发布记录")]
    public class  ReleaseLogController : ModuleController
    {
        private readonly IReleaseLogService _service;

        public  ReleaseLogController(ILoginInfo loginInfo,IReleaseLogService ReleaseLogService)
        {
            _service = ReleaseLogService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]ReleaseLogQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpGet]
        [Description("查询全部")]
        [Common]
        public Task<IResultModel> QueryAll()
        {
            return _service.QueryAll();
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(ReleaseLogAddModel model)
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
        public Task<IResultModel> Update(ReleaseLogUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]string  id)
        {
            return _service.Delete(id);
        }
    }
}

