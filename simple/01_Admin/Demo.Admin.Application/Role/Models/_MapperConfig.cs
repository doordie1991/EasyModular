using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.RoleService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<RoleAddModel, RoleEntity>();
            cfg.CreateMap<RoleEntity, RoleUpdateModel>();

            cfg.CreateMap<MenuEntity, RoleMenuPermissionTreeModel>()
               .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.Id));

            cfg.CreateMap<MenuButtonEntity, RoleMenuButtons>();
            cfg.CreateMap<RoleMenuButtons, RolePermissionEntity>();
        }
    }
}
