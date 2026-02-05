using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process
{
    public class LeaveTypeSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveID { get; set; }
        public string? LeaveCode { get; set; }
        public string? LeaveDesc { get; set; }
        public string? ChargeableTo { get; set; }
        public string? WithPay { get; set; }
        public bool? SubTypeRequired { get; set; }
        public bool? BasedOnTenure { get; set; }
        public bool? WithDateDuration { get; set; }
        public bool? NoBalance { get; set; }
        public bool? LegalFileAsLeave { get; set; }
        public bool? SphFileAsLeave { get; set; }
        public bool? DbleLegalFileAsLeave { get; set; }
        public bool? Sph2FileAsLeave { get; set; }
        public string? PrevYrLvCode { get; set; }
        public bool? NwhFileAsLeave { get; set; }
        public bool? RequiredAdvanceFiling { get; set; }
        public bool? ExemptFromAllowDeduction { get; set; }
    }
}
