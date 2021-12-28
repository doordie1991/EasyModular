using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.OrganizeService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OrganizeAddModel,OrganizeEntity>();
            cfg.CreateMap<OrganizeEntity, OrganizeUpdateModel>();
            cfg.CreateMap<OrganizeEntity, OrganizeTreeResultModel>();
        }
    }
}
