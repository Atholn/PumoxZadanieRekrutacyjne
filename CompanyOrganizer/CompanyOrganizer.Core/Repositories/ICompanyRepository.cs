using CompanyOrganizer.Core.Models;
using CompanyOrganizer.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyOrganizer.Core.Repositories
{
  public   interface ICompanyRepository
    {
        Company Get(long Id);
        List<Company> GetAll();
        long Add(Company company);
        List<Company> Search (string Keyword , DateTime? EmployeeDateOfBirthFrom, DateTime? EmployeeDateOfBirthTo , Position? EmployeeJobTitles);
        void Update(long Id, Company companyModel);
        void Delete(Company company);    
    }
}
