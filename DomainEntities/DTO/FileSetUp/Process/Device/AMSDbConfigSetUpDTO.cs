using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process.Device
{
    public class AMSDbConfigSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Description { get; set; }
        public string? Server { get; set; }
        public string? DatabaseName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime? LastDateUpdated { get; set; }
        public bool? WithDeviceCode { get; set; }
        public string? TableName { get; set; }
        public string? EmpCode { get; set; }
        public string? TimeStamp { get; set; }
        public string? Flag { get; set; }
        public string? FlagCode { get; set; }
        public bool? IsAutomaticEmpCode { get; set; }
        public string? EmployeeCodeTable { get; set; }
        public string? EmployeeCodeCol { get; set; }
        public string? EmpoyeeCodeIDCol { get; set; }
        public int DateDaysAhead { get; set; }
        public string? LastDateUpdateReplica { get; set; }
        public DateTime? LastDateUpdateTo { get; set; }
        public bool? LastDateUpdateFlag { get; set; }
        public DateTime? LastDateUpdateFrom { get; set; }
        public string? DeviceNameCol { get; set; }
    }
}
