using AutoMapper;
using EasyModular.AutoMapper;
using Demo.ModularB.Domain.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ModularB.Application.User.Models
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserAddModel, UserEntity>();
            cfg.CreateMap<UserEntity, UserUpdateModel>();
        }
    }
}
