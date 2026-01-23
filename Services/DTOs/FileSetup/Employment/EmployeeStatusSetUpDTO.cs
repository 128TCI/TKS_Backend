using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.FileSetup.Employment
{
    public class EmployeeStatusSetUpDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpStatID { get; set; }
        [Key]
        public string EmpStatCode { get; set; }
        public string EmpStatDesc { get; set; }
    }
}
