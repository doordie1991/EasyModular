using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Scheduling.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scheduling.Application.JobService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<JobAddModel,JobEntity>();
            cfg.CreateMap<JobEntity, JobUpdateModel>();
        }
    }
}
