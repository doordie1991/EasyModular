using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasyModular
{
    public class ModuleAssemblyDescriptor : IModuleAssemblyDescriptor
    {
        public Assembly Web { get; set; }

        public Assembly Application { get; set; }

        public Assembly Domain { get; set; }

        public Assembly Infrastructure { get; set; }

        public Assembly Quartz { get; set; }
    }
}
