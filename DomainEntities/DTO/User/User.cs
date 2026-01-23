using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities.DTO.User
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string MachineName { get; set; }
        public string? Password { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? IsLoggedIn { get; set; }
        public bool? IsSuspended { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public string? EmpCode { get; set; }
        public bool? IsWindowsAuthenticate { get; set; }
        public string? WindowsLoginName { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? ForgotPasswordExpiry { get; set; }
        public string? ForgotPasswordCode { get; set; }
        public string? EncryptedPass { get; set; }
        public DateTime? LastPasswordFailureDate { get; set; }
        public int? PasswordFailuresSinceLastSuccess { get; set; }
        public string? MachineIdentifier { get; set; }

        // Domain validation method
        public bool IsValid()
        {
            // Validates that non-nullable string fields in the DB are not empty
            return !string.IsNullOrWhiteSpace(UserName) &&
                   !string.IsNullOrWhiteSpace(MachineName);
        }
    }
}