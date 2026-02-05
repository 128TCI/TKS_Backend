using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process.Device
{
    public class DTRFlagSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? FlagCode { get; set; }
        public string? TimeIn { get; set; }
        public string? TimeOut { get; set; }
        public string? Break1Out { get; set; }
        public string? Break1In { get; set; }
        public string? Break2Out { get; set; }
        public string? Break2In { get; set; }
        public string? Break3Out { get; set; }
        public string? Break3In { get; set; }
    }
}
