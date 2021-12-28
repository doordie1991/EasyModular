using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular
{
    public class ModuleDescriptor : IModuleDescriptor
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 说明介绍
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 程序集
        /// </summary>
        public IModuleAssemblyDescriptor AssemblyDescriptor { get; set; }

        /// <summary>
        /// 模块初始化
        /// </summary>
        public IModuleInitializer Initializer { get; set; }
    }
}
