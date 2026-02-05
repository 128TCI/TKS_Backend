using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings
{
    public class AllowanceBracketingSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string? DayType { get; set; }

        public decimal? NoOfHrs { get; set; }

        public string? EarningCode { get; set; }

        public decimal? Amount { get; set; }

        public string? Code { get; set; }

        public string? WorkShiftCode { get; set; }

        public bool? ByEmploymentStatFlag { get; set; }

        public string? EmploymentStatus { get; set; }
    }
}
