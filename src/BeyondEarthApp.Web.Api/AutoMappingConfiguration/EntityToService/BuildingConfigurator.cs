﻿using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.EntityToService
{
    public class BuildingConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Building, Models.Building>()
                .ForMember(opt => opt.Links, x => x.Ignore());

            // Precis mapping
            mapper.CreateMap<Building, Models.Precis.BuildingPrecis>()
                .ForMember(x => x.Links, opts => opts.Ignore());
        }
    }
}