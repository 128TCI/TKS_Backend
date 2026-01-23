using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.FileSetup.Employment
{
    public class AreaSetUpDTO
    {
        public int ID { get; set; }
        public string AreaCode { get; set; }
        public string? AreaDesc { get; set; }
        public string? Head { get; set; }
        public string? HeadCode { get; set; }
        public string? AcctCode { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public string? DeviceName { get; set; }
    }
}
