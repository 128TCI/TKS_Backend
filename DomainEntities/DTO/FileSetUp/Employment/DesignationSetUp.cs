using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Employment
{
    public class DesignationSetUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DesID { get; set; }
        public string DesCode { get; set; }
        public string? DesDesc { get; set; }
        public string? RateCode { get; set; }
        public string? ClsCode { get; set; }
        public string? GrdCode { get; set; }
        public string? JobLevelCode { get; set; }
        public string? DeviceName { get; set; }
    }
}
