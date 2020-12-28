using AutoMapper;
using CompanyOrganizer.Core.Models;
using CompanyOrganizer.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyOrganizer.Infrastructure.Mappers
{
  public   class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.Name, map => map.MapFrom(company => company.Name))
                .ForMember(c => c.EstablishmentYear, map => map.MapFrom(company => company.EstablishmentYear));

            CreateMap<CompanyDto, Company>()
                .ForMember(c => c.Name, map => map.MapFrom(company => company.Name))
                .ForMember(c => c.EstablishmentYear, map => map.MapFrom(company => company.EstablishmentYear));

            CreateMap<WorkerDto, Worker>()
                .ForMember(c => c.PositionTitle, map => map.MapFrom(worker => worker.PositionTitle))
                .ReverseMap();
        }
    }
}
