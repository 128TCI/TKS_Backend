using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Employment
{
    public class GroupSetUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string GrpCode { get; set; }
        public string? GrpDesc { get; set; }
        public string? GrpHead { get; set; }
        public string? GrpDesig { get; set; }
        public string? GrpHeadCode { get; set; }
    }
}
