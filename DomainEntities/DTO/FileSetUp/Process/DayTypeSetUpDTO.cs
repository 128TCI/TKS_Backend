using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process
{
    public class DayTypeSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string? RegularDay { get; set; }
        public string? RestDay { get; set; }
        public string? LegalHoliday { get; set; }
        public string? SpecialHoliday { get; set; }
        public string? LegalHolidayFallRestDay { get; set; }
        public string? SpecialHolidayFallRestDay { get; set; }
        public string? DoubleLegalHoliday { get; set; }
        public string? DoubleLegalHolidayFallRestday { get; set; }
        public string? SpecialHoliday2 { get; set; }
        public string? SpecialHoliday2FallRestDay { get; set; }
        public string? NonWorkingHoliday { get; set; }
        public string? NonWorkingHolidayFallRestDay { get; set; }
    }
}
