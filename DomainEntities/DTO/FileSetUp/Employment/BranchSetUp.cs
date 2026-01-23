using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Employment
{
    public class BranchSetUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BraID { get; set; }
        public string BraCode { get; set; }
        public string? BraDesc { get; set; }
        public string? BraMngr { get; set; }
        public string? BraMngrCode { get; set; }
        public string? DeviceName { get; set; }
    }
}

