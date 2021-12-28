using EasyModular.Utils;
using Demo.Admin.Application.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using EasyModular;
using EasyModular.Auth;
using Demo.Admin.Application.PermissionService;

namespace Demo.Admin.Web
{
    public class PermissionValidateFilter: IPermissionValidateHandler
    {
        private readonly ILoginInfo _loginInfo;
        private readonly IPermissionService _permissionService;
        private readonly WebConfigModel _webConfigModel;

        public PermissionValidateFilter(IPermissionService permissionService, ILoginInfo loginInfo, WebConfigModel webConfigModel)
        {
            _permissionService = permissionService;
            _loginInfo = loginInfo;
            _webConfigModel = webConfigModel;
        }

        public bool Validate(IDictionary<string, string> routeValues, string httpMethod)
        {
            if (!_webConfigModel.IsValidate)
                return true;

            var permissions = _permissionService.QueryByUserId(_loginInfo.UserId).Result;

            var area = routeValues["area"];
            var controller = routeValues["controller"];
            var action = routeValues["action"];
            return permissions.Any(m => m.PermissionCode.EqualsIgnoreCase($"{area}_{controller}_{action}_{httpMethod}"));
        }
    }
}
