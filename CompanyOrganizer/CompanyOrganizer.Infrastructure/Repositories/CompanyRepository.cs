
using CompanyOrganizer.Core.Models;
using CompanyOrganizer.Core.Repositories;
using CompanyOrganizer.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyOrganizer.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
       protected readonly CompanyContext _context;

        public CompanyRepository (CompanyContext context)
        {
            _context = context;
        }
        public Company Get(long Id)
        {
            return _context.Companies
                .Include(m => m.Workers)
                .SingleOrDefault(x => x.Id == Id);
        }

        public List<Company> GetAll()
        {
            return _context.Companies
                .Include(m => m.Workers)
                 .ToList();
        }

        public long  Add(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return _context.Companies.OrderBy(company => company.Id).LastOrDefault().Id; ;
        }

        public List<Company> Search(string Keyword, DateTime EmployeeDateOfBirthFrom, DateTime EmployeeDateOfBirthTo, string EmployeeJobTitles)
        {
            return _context.Companies
                   .FromSqlRaw(
                   "SELECT c.*" +
                   " FROM dbo.Companies c " +
                   " JOIN dbo.Workers w ON w.CompanyId = c.Id" +
                   " WHERE w.Name LIKE '%" + Keyword +
                    "%' OR  w.LastName LIKE '%" + Keyword +
                    "%' OR  c.Name LIKE '%" + Keyword +
                    "%' OR w.PositionTitle Like '%" + EmployeeJobTitles +
                    "%' OR  (w.DateOfBirth < '" + EmployeeDateOfBirthTo.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                    "' AND  w.DateOfBirth > '" + EmployeeDateOfBirthFrom.ToString("yyyy-MM-dd HH:mm:ss.fff") + "') ")
                   .Include(w => w.Workers)
                  .ToList();
        }

        public void Delete(Company company)
        {
            _context.Companies.Remove(company);
            _context.SaveChanges();
        }

        public void Update(long Id, Company companyModel)
        {
            var company = _context.Companies.FirstOrDefault(m => m.Id == Id);
            company.Name = companyModel.Name;
            company.EstablishmentYear = companyModel.EstablishmentYear;
            company.UpdatedAt = DateTime.UtcNow;

            var workers =  _context.Workers.Where(w=>w.CompanyId == Id).ToList();
            _context.Workers.RemoveRange(workers);
            company.Workers = new List<Worker>(companyModel.Workers);
            _context.SaveChanges();
        }     
    }
}
