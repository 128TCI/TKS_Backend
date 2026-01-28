using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.System
{
    public class CompanyInformationConfigDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? NumberOfAttempts { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NumberOfSeconds { get; set; }

        public int? PasswordAge { get; set; }
    }
}