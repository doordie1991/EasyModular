using EasyModular.Auth;
using EasyModular.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Demo.ModularA.Application.Organization;
using Demo.ModularA.Application.Organization.Models;
using Demo.ModularA.Domain.Organization.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Demo.ModularA.Api.Controllers
{

    [Description("组织")]
    public class OrganizationController : ModuleController
    {
        private readonly IOrganizationService _service;

        public OrganizationController(IOrganizationService OrganizationService)
        {
            _service = OrganizationService;
        }

        [HttpGet]
        [Description("查询")]
        [AllowAnonymous]
        public Task<IResultModel> Query([FromQuery]OrganizationQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        [AllowAnonymous]
        public Task<IResultModel> Add(OrganizationAddModel model)
        {
            return _service.Add(model);
        }

        [HttpGet]
        [Description("编辑")]
        [AllowAnonymous]
        public Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("更新")]
        [AllowAnonymous]
        public Task<IResultModel> Update(OrganizationUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpDelete]
        [Description("删除")]
        [AllowAnonymous]
        public Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return _service.Delete(id);
        }
    }
}
