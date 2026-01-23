using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.FileSetup.Employment
{
    public class DesignationSetUpDTO
    {
        public int DesID { get; set; }
        public string DesCode { get; set; }
        public string DesDesc { get; set; }
        public string RateCode { get; set; }
        public string ClsCode { get; set; }
        public string GrdCode { get; set; }
        public string JobLevelCode { get; set; }
        public string DeviceName { get; set; }
    }
}
