using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.FileSetup.Employment
{
    public class JobLevelSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobLevelID { get; set; }
        public string JobLevelCode { get; set; }
        public string JobLevelDesc { get; set; }
    }
}
