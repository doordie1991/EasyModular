using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.EnterpriseService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TenantAddModel,TenantEntity>();
            cfg.CreateMap<TenantEntity, TenantUpdateModel>();
            cfg.CreateMap<TenantEntity, TenantTreeResultModel>();
        }
    }
}
