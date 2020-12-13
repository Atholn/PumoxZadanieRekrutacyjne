using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompanyOrganizer.Core.Models
{
    public class Company : Entity
    {
        [Required(ErrorMessage = "Company must have a name")]
        [MinLength(3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Company must have a Establishment Year")]
        public int EstablishmentYear { get; set; }
        [Required(ErrorMessage ="Company must have worksers")]
        public virtual List<Worker> Workers { get; set; }
    }
}
