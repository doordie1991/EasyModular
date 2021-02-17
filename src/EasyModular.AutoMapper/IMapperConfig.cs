using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace EasyModular.AutoMapper
{
    /// <summary>
    /// 对象映射绑定
    /// </summary>
    public interface IMapperConfig
    {
        /// <summary>
        /// 绑定
        /// </summary>
        /// <param name="cfg"></param>
        void Bind(IMapperConfigurationExpression cfg);
    }
}
