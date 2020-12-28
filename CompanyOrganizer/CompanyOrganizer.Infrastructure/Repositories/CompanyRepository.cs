using CompanyOrganizer.Core.Models;
using CompanyOrganizer.Core.Models.Enums;
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

        public CompanyRepository(CompanyContext context)
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

        public long Add(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return _context.Companies.OrderBy(company => company.Id).LastOrDefault().Id; ;
        }

        public List<Company> Search(string Keyword, DateTime? EmployeeDateOfBirthFrom, DateTime? EmployeeDateOfBirthTo, Position? EmployeeJobTitles)
        {
            return _context.Workers.Join(
                  _context.Companies,
                  company => company.CompanyId,
                  worker => worker.Id,
                  (worker, company) => new
                  {
                      Id = worker.Id,
                      Company = company,
                      CompanyId = company.Id,
                      Workers = company.Workers,
                      DateOfBirth = worker.DateOfBirth,
                      LastName = worker.LastName,
                      Name = worker.Name,
                      PositionTitle = worker.PositionTitle
                  })
                   .Where(x =>
                   (Keyword == null ||
                   x.Company.Name.Contains(Keyword) ||
                   x.Name.Contains(Keyword) ||
                   x.LastName.Contains(Keyword) ||
                   x.PositionTitle == EmployeeJobTitles || 
                   (x.DateOfBirth >= EmployeeDateOfBirthFrom.Value &&
                   x.DateOfBirth <= EmployeeDateOfBirthTo.Value))) 
                   .ToList()
                   .Select(x => new Company
                 {
                     Id = x.Company.Id,
                     CreatedAt = x.Company.CreatedAt,
                     UpdatedAt = x.Company.UpdatedAt,
                     EstablishmentYear = x.Company.EstablishmentYear,
                     Name = x.Company.Name,
                     Workers = x.Company.Workers
                 })
                   .GroupBy(x => x.Id)
                   .Select(x => x.FirstOrDefault())      
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
