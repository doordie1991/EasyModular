using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Swagger
{
    /// <summary>
    /// API分组约定
    /// </summary>
    public class ApiExplorerGroupConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            var arr = controllerNamespace.Split('.');
            var moduleName =$"{arr[0]}.{arr[1]}";

            controller.ApiExplorer.GroupName = moduleName;
        }
    }
}
