using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.FileSetup.Employment
{
    public class BranchSetUpDTO
    {
        public int BraID { get; set; }
        public string BraCode { get; set; }
        public string? BraDesc { get; set; }
        public string? BraMngr { get; set; }
        public string? BraMngrCode { get; set; }
        public string? DeviceName { get; set; }
    }
}
