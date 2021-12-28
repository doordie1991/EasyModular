
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.MenuButtonService;
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
    [Description("菜单按钮")]
    public class  MenuButtonController : ModuleController
    {
        private readonly ILoginInfo _loginInfo;
        private readonly IMenuButtonService _service;

        public  MenuButtonController(ILoginInfo loginInfo,IMenuButtonService MenuButtonService)
        {
            _loginInfo = loginInfo;
            _service = MenuButtonService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]MenuButtonQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpGet]
        [Description("根据菜单Id查询")]
        public Task<IResultModel> QueryByMenuId([BindRequired]string  menuId)
        {
            return _service.QueryByMenuId(menuId);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(MenuButtonAddModel model)
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
        public Task<IResultModel> Update(MenuButtonUpdateModel model)
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

