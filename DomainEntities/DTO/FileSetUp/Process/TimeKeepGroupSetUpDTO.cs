using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process
{
    public class TimeKeepGroupSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string GroupCode { get; set; } = string.Empty;

        public string? GroupDescription { get; set; }

        public string? PayrollGroup { get; set; }

        public DateTime? CutOffDateFrom { get; set; }

        public DateTime? CutOffDateTo { get; set; }

        public string? CutOffDateMonth { get; set; }

        public string? CutOffDatePeriod { get; set; }

        public bool? IntegrationPayroll { get; set; }

        public bool? IntegrationHRIS { get; set; }

        public string? PreparedBy { get; set; }

        public string? PreparedByPostion { get; set; }

        public string? CheckedBy { get; set; }

        public string? CheckedByPosition { get; set; }

        public string? NotedBy { get; set; }

        public string? NotedByPosition { get; set; }

        public string? ApprovedBy { get; set; }

        public string? ApprovedByPosition { get; set; }

        public string? TerminalID { get; set; }

        public DateTime? AutoPairLogsDateFrom { get; set; }

        public DateTime? AutoPairLogsDateTo { get; set; }
    }
}
