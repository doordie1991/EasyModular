using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Scheduling.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scheduling.Application.JobLogService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<JobLogAddModel,JobLogEntity>();
            cfg.CreateMap<JobLogEntity, JobLogUpdateModel>();
        }
    }
}
