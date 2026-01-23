using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Employment
{
    public class DepartmentSetUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepID { get; set; }
        public string DepCode { get; set; }
        public string DivCode { get; set; }
        public string DepDesc { get; set; }
        public string DepHead { get; set; }
        public string DepHeadCode { get; set; }
        public string Head1 { get; set; }
        public string Head2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string DeviceName { get; set; }
    }
}
