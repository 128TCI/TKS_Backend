using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process.Device
{
    public class SDKListSetUpDTO
    {
        public int ID { get; set; }
        public string? IPAdd { get; set; }
        public string? Port { get; set; }
        public int? MachID { get; set; }
        public bool? wDeviceCode { get; set; }
        public string? FlagCode { get; set; }
    }
}
