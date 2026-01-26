using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Employment
{
    public class PayHouseSetUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineID { get; set; }
        public string LineCode { get; set; }
        public string? LineDesc { get; set; }
        public string? Head { get; set; }
        public string? Position { get; set; }
        public string? HeadCode { get; set; }
        public string? DeviceName { get; set; }

    }
}
