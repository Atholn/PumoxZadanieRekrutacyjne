using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompanyOrganizer.Infrastructure.DTO
{
    public class CompanyDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public int EstablishmentYear { get; set; }
        [Required]
        public List<WorkerDto> Workers { get; set; }
    }
}
