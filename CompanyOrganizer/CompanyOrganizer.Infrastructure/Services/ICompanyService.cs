using CompanyOrganizer.Core.Models;
using CompanyOrganizer.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyOrganizer.Infrastructure.Services
{
   public  interface ICompanyService
    {
        CompanyDto Get(long Id);
        List<CompanyDto> GetAll();
        long Create(CompanyDto companyDto);
        List<CompanyDto> Search(SearchDto searchDto);
        void Update(long Id, CompanyDto companyDto);
        void Delete(long Id);
   
    }
}
