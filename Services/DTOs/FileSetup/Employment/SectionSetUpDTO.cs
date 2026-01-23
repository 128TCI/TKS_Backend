using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.FileSetup.Employment
{
    public class SectionSetUpDTO
    {
        public int SecID { get; set; }
        public string SecCode { get; set; }
        public string DepCode { get; set; }
        public string SecDesc { get; set; }
        public string SecHead { get; set; }
        public string SecHeadCode { get; set; }
        public string DeviceName { get; set; }
    }
}
