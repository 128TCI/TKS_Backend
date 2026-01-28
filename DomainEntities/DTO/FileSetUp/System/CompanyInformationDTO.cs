using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.System
{
    public class CompanyInformationDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyID { get; set; }

        public string CompanyCode { get; set; }

        public string? CompanyName { get; set; }

        public byte[]? CompanyLogo { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Province { get; set; }

        public string? ZipCode { get; set; }

        public string? TelNo { get; set; }

        public string? Email { get; set; }

        public string? SSSNo { get; set; }

        public string? PhilHealthNo { get; set; }

        public string? Pag_Ibig { get; set; }

        public string? TIN { get; set; }

        public string? BIR_BRNCode { get; set; }

        public DateTime? BusFr { get; set; }

        public DateTime? BusTo { get; set; }

        public bool? TimeInTimeOutScreen { get; set; }

        public bool? MilitaryTime { get; set; }

        public int? DecPlaces { get; set; }

        public byte[]? WebLogo { get; set; }

        public string? WebLogoType { get; set; }

        public byte[]? WebLogoReports { get; set; }

        public string? WebLogoReportsType { get; set; }

        public string? Line1 { get; set; }

        public string? Line2 { get; set; }

        public string? Head { get; set; }

        public string? ChartAcct { get; set; }

        public string? PayrollPath { get; set; }

        public string? HRISPath { get; set; }

        public bool? OTPremiumFlag { get; set; }

        public bool? TerminalID { get; set; }

        public bool? ValidateLogs { get; set; }

        public bool? ReadOnlyTxtDate { get; set; }

        public string? Policy { get; set; }

        public bool? Flag { get; set; }

        public string? GSISNo { get; set; }

        public bool? ExportEmail { get; set; }

        public byte[]? SiteLogo { get; set; }

        public byte[]? SiteContent { get; set; }

        public bool? PasswordHistory { get; set; }

        public string? TKSPhotoPath { get; set; }

        public bool? ExportLateFilingDateFlag { get; set; }

        public bool? EnableAutoPairingLogsFlag { get; set; }

        public bool? EnableAppOTRawDataFlag { get; set; }

        public bool? Enable2ndShiftRawDataFlag { get; set; }
         
    }
}
