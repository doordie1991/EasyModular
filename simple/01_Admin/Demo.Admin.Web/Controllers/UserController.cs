
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.UserService;
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
    [Description("用户")]
    public class  UserController : ModuleController
    {
        private readonly IUserService _service;
        private readonly IExcelHandler _excelHandler;
        public  UserController(ILoginInfo loginInfo,IUserService UserService, IExcelHandler excelHandler)
        {
            _service = UserService;
            _excelHandler = excelHandler;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]UserQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpGet]
        [Description("查询同组织")]
        [Common]
        public Task<IResultModel> QueryBySameOrg([FromQuery] UserQueryModel model)
        {
            return _service.QueryBySameOrg(model);
        }

        [HttpGet]
        [Description("查询最近选择")]
        [Common]
        public Task<IResultModel> QueryLatestSelect([FromQuery] UserQueryModel model)
        {
            return _service.QueryLatestSelect(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(UserAddModel model)
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
        public Task<IResultModel> Update(UserUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpPost]
        [Description("启用")]
        public Task<IResultModel> Enable([BindRequired] string  id)
        {
            return _service.Enable(id);
        }

        [HttpPost]
        [Description("更新密码")]
        [Common]
        public Task<IResultModel> UpdatePassword(UpdatePasswordModel model)
        {
            return _service.UpdatePassword(model);
        }

        [HttpPost]
        [Description("移动")]
        public Task<IResultModel> Move(UserMoveModel model)
        {
            return _service.Move(model);
        }

        [HttpPost]
        [Description("禁用")]
        public Task<IResultModel> Disable([BindRequired] string  id)
        {
            return _service.Disable(id);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]string  id)
        {
            return _service.Delete(id);
        }

        [HttpGet]
        [Description("下拉")]
        public Task<IResultModel> Select(string keywords)
        {
            return _service.Select(keywords);
        }

        [HttpPost]
        [Description("根据多个账号查询")]
        [Common]
        public Task<IResultModel> QueryByUserCodes(List<string> userCodes)
        {
            return _service.QueryByUserCodes(userCodes);
        }

        [HttpPost]
        [Description("根据多个用户Id查询")]
        [Common]
        public Task<IResultModel> QueryByUserIds(List<string> userIds)
        {
            return _service.QueryByUserIds(userIds);
        }

        [HttpPost]
        [Description("保存用户最近选择")]
        [Common]
        public Task<IResultModel> SaveLatestSelect(List<string > ids)
        {
            return _service.SaveLatestSelect(ids);
        }

        [HttpGet]
        [Description("用户树形列表")]
        [Common]
        public Task<IResultModel> Tree()
        {
            return _service.GetTree();
        }
    }
}

