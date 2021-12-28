 
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Scheduling.Application;
using Demo.Scheduling.Application.JobGroupService;
using Demo.Scheduling.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Web
{
    [Description("任务分组")]
    public class  JobGroupController : ModuleController
    {
        private readonly IJobGroupService _service;

        public  JobGroupController(ILoginInfo loginInfo,IJobGroupService JobGroupService)
        {
            _service = JobGroupService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]JobGroupQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(JobGroupAddModel model)
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
        public Task<IResultModel> Update(JobGroupUpdateModel model)
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
        [Description("下拉")]
        public Task<IResultModel> Select()
        {
            return _service.Select();
        }
    }
}

