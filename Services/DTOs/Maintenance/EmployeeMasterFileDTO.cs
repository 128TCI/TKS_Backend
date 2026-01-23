using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Maintenance
{
    public class EmployeeMasterFileDTO
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string? EmpStatCode { get; set; }
        public string? Courtesy { get; set; }
        public string? LName { get; set; }
        public string? FName { get; set; }
        public string? MName { get; set; }
        public string? NickName { get; set; }
        public string? HAddress { get; set; }
        public string? PAddress { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public string? CivilStatus { get; set; }
        public string? Citizenship { get; set; }
        public string? Religion { get; set; }
        public string? Sex { get; set; }
        public string? Email { get; set; }
        public string? Weight { get; set; }
        public string? Height { get; set; }
        public string? MobilePhone { get; set; }
        public string? HomePhone { get; set; }
        public string? PresentPhone { get; set; }
        public DateTime? DateHired { get; set; }
        public DateTime? DateRegularized { get; set; }
        public DateTime? DateResigned { get; set; }
        public DateTime? DateSuspended { get; set; }
        public DateTime? ProbeStart { get; set; }
        public DateTime? ProbeEnd { get; set; }
        public bool? Suspend { get; set; }
        public bool? Separated { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Age { get; set; }
        public string? BirthPlace { get; set; }
        public bool? UnionMember { get; set; }
        public bool? Agency { get; set; }
        public string? DivCode { get; set; }
        public string? DepCode { get; set; }
        public string? SecCode { get; set; }
        public string? GrpCode { get; set; }
        public string? BraCode { get; set; }
        public string? SubAcctCode { get; set; }
        public string? DesCode { get; set; }
        public string? ShiftCode { get; set; }
        public string? Superior { get; set; }
        public string? GrdCode { get; set; }
        public string? ClsCode { get; set; }
        public string? PayCode { get; set; }
        public int? LocId { get; set; }
        public string? RateCode { get; set; }
        public decimal? TaxID { get; set; }
        public string? TaxCode { get; set; }
        public string? BankAccount { get; set; }
        public string? BankCode { get; set; }
        public string? SSSNo { get; set; }
        public string? PHilHealthNo { get; set; }
        public string? PagIbigNo { get; set; }
        public string? TIN { get; set; }
        public string? PagibigCode { get; set; }
        public byte[]? photo { get; set; }
        public string? CatCode { get; set; }
        public string? UnitCode { get; set; }
        public bool? Contractual { get; set; }
        public string? AreaCode { get; set; }
        public string? LocCode { get; set; }
        public string? GSISNo { get; set; }
        public string? Suffix { get; set; }
        public string? OnlineAppCode { get; set; }
    }
}
