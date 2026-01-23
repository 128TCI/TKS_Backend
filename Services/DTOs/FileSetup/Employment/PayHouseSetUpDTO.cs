using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.FileSetup.Employment
{
    public class PayHouseSetUpDTO
    {
        public int LineID { get; set; }
        public string LineCode { get; set; }
        public string LineDesc { get; set; }
        public string Head { get; set; }
        public string Position { get; set; }
        public string HeadCode { get; set; }
        public string DeviceName { get; set; }
    }
}
