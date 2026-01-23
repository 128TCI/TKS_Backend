using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Employment
{
    public class UnitSetUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitID { get; set; }
        public string UnitCode { get; set; }
        public string UnitDesc { get; set; }
        public string Head { get; set; }
        public string Position { get; set; }
        public string DeviceName { get; set; }
    }
}
