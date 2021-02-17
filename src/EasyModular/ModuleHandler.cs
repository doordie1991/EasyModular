using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace EasyModular
{
    public class ModuleHandler : IModuleHandler
    {
        public IList<IModuleDescriptor> LoadModules()
        {
            var moduleDescriptors = GetConfigInfo<ModuleDescriptor>(Path.Combine(AppContext.BaseDirectory, "config/module.json"));

            var modules = new List<IModuleDescriptor>();

            foreach (var item in moduleDescriptors)
            {
                if (modules.Any(m => m.Name.Equals(item.Name)))
                    continue;

                //加载程序集信息
                var assemblyDescriptor = new ModuleAssemblyDescriptor
                {
                    Api = AssemblyHelper.LoadByName($"{item.Id}.Api.dll"),
                    Application = AssemblyHelper.LoadByName($"{item.Id}.Application.dll"),
                    Domain = AssemblyHelper.LoadByName($"{item.Id}.Domain.dll"),
                    Infrastructure = AssemblyHelper.LoadByName($"{item.Id}.Infrastructure.dll"),
                    Quartz = AssemblyHelper.LoadByName($"{item.Id}.Quartz.dll")
                   
                };

                item.AssemblyDescriptor = assemblyDescriptor;

                //加载模块初始化器
                if (assemblyDescriptor.Api != null)
                {
                    var initializerType = assemblyDescriptor.Api.GetTypes().FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t));
                    if (initializerType != null && (initializerType != typeof(IModuleInitializer)))
                    {
                        item.Initializer = (IModuleInitializer)Activator.CreateInstance(initializerType);
                    }
                }

                modules.Add(item);
            }

            return modules;
        }

        /// <summary>
        /// 根据路径获取配置文件内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        private List<T> GetConfigInfo<T>(string url)
        {
            using var jsonReader = new StreamReader(url);
            var models = JsonConvert.DeserializeObject<List<T>>(jsonReader.ReadToEnd());
            return models;
        }
    }
}
