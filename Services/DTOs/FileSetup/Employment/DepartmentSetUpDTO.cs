using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.FileSetup.Employment
{
    public class DepartmentSetUpDTO
    {
        public int DepID { get; set; }
        public string DepCode { get; set; }
        public string? DivCode { get; set; }
        public string? DepDesc { get; set; }
        public string? DepHead { get; set; }
        public string? DepHeadCode { get; set; }
        public string? Head1 { get; set; }
        public string? Head2 { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public string? DeviceName { get; set; }
    }
}
