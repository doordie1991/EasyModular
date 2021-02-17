using AutoMapper;
using EasyModular.AutoMapper;
using Demo.ModularB.Domain.Organization;
using System;
using System.Collections.Generic;
using System.Text;
using Demo.ModularB.Domain.Organization.Models;

namespace Demo.ModularB.Application.Organization.Models
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OrganizationAddModel, OrganizationEntity>();
            cfg.CreateMap<OrganizationEntity, OrganizationUpdateModel>();
        }
    }
}
