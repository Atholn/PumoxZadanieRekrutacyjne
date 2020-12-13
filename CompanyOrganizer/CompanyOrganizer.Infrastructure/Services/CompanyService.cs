using AutoMapper;
using CompanyOrganizer.Core.Models;
using CompanyOrganizer.Core.Repositories;
using CompanyOrganizer.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyOrganizer.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public CompanyDto Get(long Id)
        {
            var company = _companyRepository.Get(Id);
            return _mapper.Map<CompanyDto>(company);
        }

        public List<CompanyDto> GetAll()
        {
            var companys = _companyRepository.GetAll();
            return _mapper.Map<List<CompanyDto>>(companys);
        }

        public long Create(CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                throw new Exception($"There is no company with this ID");
            }
               
            Company company = _mapper.Map<Company>(companyDto);
            var key = _companyRepository.Add(company);
            return key;
        }

        public void Delete(long Id)
        {
            var company = _companyRepository.Get(Id);
            if (company == null)
            {
                throw new Exception($"There is no company with this ID");
            }
            _companyRepository.Delete(company);
        }

        public  List<CompanyDto> Search(SearchDto searchDto)
        {
            var companies = _companyRepository.Search(searchDto.Keyword, searchDto.EmployeeDateOfBirthFrom, searchDto.EmployeeDateOfBirthTo, searchDto.EmployeeJobTitles);
            return   _mapper.Map<List<CompanyDto>>(companies);
        }

        public void Update(long Id, CompanyDto companyDto)
        {
            _companyRepository.Update(Id, _mapper.Map<Company>(companyDto));
        }
    }
}
