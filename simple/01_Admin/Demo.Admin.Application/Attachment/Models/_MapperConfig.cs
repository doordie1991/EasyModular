using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.AttachmentService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AttachmentAddModel,AttachmentEntity>();
            cfg.CreateMap<AttachmentEntity, AttachmentUpdateModel>();

            cfg.CreateMap<AttachmentEntity, AttachmentUploadResultModel>();
        }
    }
}
