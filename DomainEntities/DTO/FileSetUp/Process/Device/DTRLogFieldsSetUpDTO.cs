using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process.Device
{
    public class DTRLogFieldsSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? DeviceFormat { get; set; }
        public string? FlagCode { get; set; }
        public int? EmpCodePos { get; set; }
        public int? EmpCodeNoOfChar { get; set; }
        public int? DatePos { get; set; }
        public int? DateNoOfChar { get; set; }
        public int? TimePos { get; set; }
        public int? TimeNoOfChar { get; set; }
        public int? FlagPos { get; set; }
        public int? FlagNoOfChar { get; set; }
        public int? TerminalPos { get; set; }
        public int? TerminalNoOfChar { get; set; }
        public string? DeviceType { get; set; }
        public int? MonthPos { get; set; }
        public int? MonthNoOfChar { get; set; }
        public int? DayPos { get; set; }
        public int? DayNoOfChar { get; set; }
        public int? YearPos { get; set; }
        public int? YearNoOfChar { get; set; }
        public int? HourPos { get; set; }
        public int? HourNoOfChar { get; set; }
        public int? MinutesPos { get; set; }
        public int? MinutesNoOfChar { get; set; }
        public int? TimePeriodPos { get; set; }
        public int? TimePeriodNoOfChar { get; set; }
        public bool? CombineDateTime { get; set; }
        public string? DateFormat { get; set; }
        public string? DateSeparator { get; set; }
        public int? IdentifierPos { get; set; }
        public int? IdentifierNoOfChar { get; set; }
    }
}
