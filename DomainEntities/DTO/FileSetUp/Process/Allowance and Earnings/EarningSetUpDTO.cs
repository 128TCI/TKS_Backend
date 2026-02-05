using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings
{
    public class EarningSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EarnID { get; set; }
        public string EarnCode { get; set; }
        public string? EarnDesc { get; set; }
        public string? EarnType { get; set; }
        public string? SysId { get; set; }
    }
}
