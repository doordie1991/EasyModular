
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.OrganizeService;
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
    [Description("组织架构")]
    public class  OrganizeController : ModuleController
    {
        private readonly IOrganizeService _service;

        public  OrganizeController(ILoginInfo loginInfo,IOrganizeService OrganizeService)
        {
            _service = OrganizeService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]OrganizeQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(OrganizeAddModel model)
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
        public Task<IResultModel> Update(OrganizeUpdateModel model)
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
        [Description("移动")]
        public Task<IResultModel> Move(OrganizeMoveModel model)
        {
            return _service.Move(model);
        }

        [HttpGet]
        [Description("组织架构树")]
        public Task<IResultModel> Tree()
        {
            return _service.GetTree();
        }
    }
}

