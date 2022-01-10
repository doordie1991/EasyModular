using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.AuditService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MenuEntity, UserMenuItem>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MenuName))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.MenuType))
                .ForMember(dest => dest.Show, opt => opt.MapFrom(src => src.IsShow))
                .ForMember(dest => dest.Target, opt => opt.MapFrom(src => (short)src.MenuTarget));
        }
    }
}
