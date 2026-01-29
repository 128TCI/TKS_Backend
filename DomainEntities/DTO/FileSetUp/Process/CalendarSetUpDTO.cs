using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process
{
    public class CalendarSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Year { get; set; }
        public string? Day { get; set; }
        public string? Month { get; set; }
        public string? Description { get; set; }
        public string? HolidayType { get; set; }
        public string? Branch { get; set; }
        public string? Time { get; set; }
        public DateTime? Tdate { get; set; }
        public DateTime? Time2 { get; set; }
    }
}
