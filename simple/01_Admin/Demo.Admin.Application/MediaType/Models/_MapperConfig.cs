using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.MediaTypeService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MediaTypeAddModel,MediaTypeEntity>();
            cfg.CreateMap<MediaTypeImportModel, MediaTypeEntity>();
            cfg.CreateMap<MediaTypeEntity, MediaTypeUpdateModel>();
        }
    }
}
