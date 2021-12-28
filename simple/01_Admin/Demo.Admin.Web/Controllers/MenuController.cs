
using EasyModular.Auth;
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.MenuService;
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
    [Description("菜单")]
    public class  MenuController : ModuleController
    {
        private readonly IMenuService _service;

        public  MenuController(ILoginInfo loginInfo,IMenuService MenuService)
        {
            _service = MenuService;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]MenuQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(MenuAddModel model)
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
        public Task<IResultModel> Update(MenuUpdateModel model)
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
        public Task<IResultModel> Move(MenuMoveModel model)
        {
            return _service.Move(model);
        }

        [HttpGet]
        [Description("菜单树")]
        public Task<IResultModel> Tree()
        {
            return _service.GetTree();
        }

        [HttpPost]
        [Description("绑定按钮")]
        public Task<IResultModel> BindBtn(MenuButtonBindModel model)
        {
            return _service.BindBtn(model);
        }
    }
}

