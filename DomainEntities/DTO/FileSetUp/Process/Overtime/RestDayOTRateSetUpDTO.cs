using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process.Overtime
{
    public class RestDayOTRateSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Code { get; set; }
        public string? Desc { get; set; } // Property name now matches SQL column exactly
        public string? WithinTheShift { get; set; }
        public string? AfterTheShift { get; set; }
        public string? WithinTheShiftWithND { get; set; }
        public string? AfterTheShiftWithND { get; set; }
        public string? OTPremiumWithinTheShiftWithND { get; set; }
        public string? OTPremiumAfterTheShiftWithND { get; set; }
        public string? EquiOTCode { get; set; }
        public string? EqOTCodeAftrShfForNoOfHrs { get; set; }
        public string? EqOTCodeAftrShfNDForNoOfHrs { get; set; }
    }
}
