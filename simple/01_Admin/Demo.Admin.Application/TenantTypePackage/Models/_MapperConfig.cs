using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.TenantTypePackageService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TenantTypePackageAddModel,TenantTypePackageEntity>();
            cfg.CreateMap<TenantTypePackageEntity, TenantTypePackageUpdateModel>();
        }
    }
}
