using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.MenuButtonService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MenuButtonAddModel,MenuButtonEntity>();
            cfg.CreateMap<MenuButtonEntity, MenuButtonUpdateModel>();
        }
    }
}
