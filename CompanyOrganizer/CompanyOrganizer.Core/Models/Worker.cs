using CompanyOrganizer.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompanyOrganizer.Core.Models
{
    public class Worker : Entity
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Position PositionTitle { get; set; }

        [Required]
        public virtual Company Company { get; set; }
        [Required]
        public long CompanyId { get; set; }
    }
}
