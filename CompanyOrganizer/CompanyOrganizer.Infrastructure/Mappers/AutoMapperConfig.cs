using AutoMapper;
using CompanyOrganizer.Core.Models;
using CompanyOrganizer.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyOrganizer.Infrastructure.Mappers
{
   public static class AutoMapperConfig
    {
        public static IMapper Initialize() => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Company, CompanyDto>();
            cfg.CreateMap<CompanyDto, Company>();
            cfg.CreateMap<Worker, WorkerDto>();
            cfg.CreateMap<WorkerDto, Worker>();

        }).CreateMapper();
    }
}
