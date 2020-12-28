using CompanyOrganizer.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyOrganizer.Infrastructure.DTO
{
   public class SearchDto
    {
        public string Keyword { get; set; }
        public DateTime EmployeeDateOfBirthFrom { get; set; }
        public DateTime EmployeeDateOfBirthTo { get; set; }
        public String EmployeeJobTitles { get; set; }
    }
}
