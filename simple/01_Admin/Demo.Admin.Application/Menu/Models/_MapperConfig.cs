using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.MenuService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MenuAddModel,MenuEntity>();
            cfg.CreateMap<MenuEntity, MenuUpdateModel>();
            cfg.CreateMap<MenuEntity, MenuTreeResultModel>();
        }
    }
}
