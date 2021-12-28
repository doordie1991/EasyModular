using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.UserService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserEntity, UserViewModel>();
            cfg.CreateMap<UserAddModel,UserEntity>();
            cfg.CreateMap<UserEntity, UserAddModel>();
            cfg.CreateMap<UserEntity, UserUpdateModel>();
            cfg.CreateMap<UserEntity, UserExportModel>();
        }
    }
}
