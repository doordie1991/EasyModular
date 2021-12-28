 
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Scheduling.Application;
using Demo.Scheduling.Application.JobService;
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
    [Description("任务")]
    public class  JobController : ModuleController
    {
        private readonly IJobService _service;

        public  JobController(ILoginInfo loginInfo,IJobService JobService)
        {
            _service = JobService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]JobQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(JobAddModel model)
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
        public Task<IResultModel> Update(JobUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]string id)
        {
            return _service.Delete(id);
        }

        [HttpPost]
        [Description("暂停")]
        public Task<IResultModel> Pause([BindRequired] string id)
        {
            return _service.Pause(id);
        }

        [HttpPost]
        [Description("恢复")]
        public Task<IResultModel> Resume([BindRequired] string id)
        {
            return _service.Resume(id);
        }

        [HttpPost]
        [Description("停止")]
        public Task<IResultModel> Stop([BindRequired] string id)
        {
            return _service.Stop(id);
        }
    }
}

