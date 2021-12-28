using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.DictionaryItemService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DictionaryItemAddModel,DictionaryItemEntity>();
            cfg.CreateMap<DictionaryItemEntity, DictionaryItemUpdateModel>();

            cfg.CreateMap<DictionaryItemEntity, DictionaryItemTreeResultModel>();
            cfg.CreateMap<DictionaryItemImportModel, DictionaryItemEntity>();
        }
    }
}
