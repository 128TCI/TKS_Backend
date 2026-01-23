using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Employment
{
    public class SectionSetUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SecID { get; set; }
        public string SecCode { get; set; }
        public string DepCode { get; set; }
        public string SecDesc { get; set; }
        public string SecHead { get; set; }
        public string SecHeadCode { get; set; }
        public string DeviceName { get; set; }
    }
}
