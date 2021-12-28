using AutoMapper;
using EasyModular.AutoMapper;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application.WorkPlanService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<WorkPlanAddModel,WorkPlanEntity>();
            cfg.CreateMap<WorkPlanEntity, WorkPlanUpdateModel>();
        }
    }
}
