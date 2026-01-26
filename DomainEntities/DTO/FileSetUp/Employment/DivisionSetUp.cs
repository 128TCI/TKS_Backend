using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Employment
{
    public class DivisionSetUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DivID { get; set; }
        public string DivCode { get; set; }
        public string? DivDesc { get; set; }
        public string? DivHead { get; set; }
        public string? DivHeadCode { get; set; }
        public string? DeviceName { get; set; }
    }
}
