using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.FileSetUp.System;
using Infrastructure.IRepositories.FileSetUp.Employment;
using Infrastructure.IRepositories.FileSetUp.System;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Employment;
using Services.Interfaces.FileSetUp.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.System
{
    public class CompanyInfromationService : ICompanyInformationService
    {
        private readonly ICompanyInformationRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public CompanyInfromationService(ICompanyInformationRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<CompanyInformationDTO> CreateAsync(CompanyInformationDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<CompanyInformationDTO> UpdateAsync(CompanyInformationDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<CompanyInformationDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<CompanyInformationDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<CompanyInformationDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private CompanyInformationDTO MapToEntity(CompanyInformationDTO dto)
        {
            return new CompanyInformationDTO
            {
                CompanyID = dto.CompanyID,
                CompanyCode = dto.CompanyCode,
                CompanyName = dto.CompanyName,
                CompanyLogo = dto.CompanyLogo,
                Address = dto.Address,
                City = dto.City,
                Province = dto.Province,
                ZipCode = dto.ZipCode,
                TelNo = dto.TelNo,
                Email = dto.Email,
                SSSNo = dto.SSSNo,
                PhilHealthNo = dto.PhilHealthNo,
                Pag_Ibig = dto.Pag_Ibig,
                TIN = dto.TIN,
                BIR_BRNCode = dto.BIR_BRNCode,
                BusFr = dto.BusFr,
                BusTo = dto.BusTo,
                TimeInTimeOutScreen = dto.TimeInTimeOutScreen,
                MilitaryTime = dto.MilitaryTime,
                DecPlaces = dto.DecPlaces,
                WebLogo = dto.WebLogo,
                WebLogoType = dto.WebLogoType,
                WebLogoReports = dto.WebLogoReports,
                WebLogoReportsType = dto.WebLogoReportsType,
                Line1 = dto.Line1,
                Line2 = dto.Line2,
                Head = dto.Head,
                ChartAcct = dto.ChartAcct,
                PayrollPath = dto.PayrollPath,
                HRISPath = dto.HRISPath,
                OTPremiumFlag = dto.OTPremiumFlag,
                TerminalID = dto.TerminalID,
                ValidateLogs = dto.ValidateLogs,
                ReadOnlyTxtDate = dto.ReadOnlyTxtDate,
                Policy = dto.Policy,
                Flag = dto.Flag,
                GSISNo = dto.GSISNo,
                ExportEmail = dto.ExportEmail,
                SiteLogo = dto.SiteLogo,
                SiteContent = dto.SiteContent,
                PasswordHistory = dto.PasswordHistory,
                TKSPhotoPath = dto.TKSPhotoPath,
                ExportLateFilingDateFlag = dto.ExportLateFilingDateFlag,
                EnableAutoPairingLogsFlag = dto.EnableAutoPairingLogsFlag,
                EnableAppOTRawDataFlag = dto.EnableAppOTRawDataFlag,
                Enable2ndShiftRawDataFlag = dto.Enable2ndShiftRawDataFlag
            };
        }

        private CompanyInformationDTO MapToDTO(CompanyInformationDTO entity)
        {
            return new CompanyInformationDTO
            {
                CompanyID = entity.CompanyID,
                CompanyCode = entity.CompanyCode,
                CompanyName = entity.CompanyName,
                CompanyLogo = entity.CompanyLogo,
                Address = entity.Address,
                City = entity.City,
                Province = entity.Province,
                ZipCode = entity.ZipCode,
                TelNo = entity.TelNo,
                Email = entity.Email,
                SSSNo = entity.SSSNo,
                PhilHealthNo = entity.PhilHealthNo,
                Pag_Ibig = entity.Pag_Ibig,
                TIN = entity.TIN,
                BIR_BRNCode = entity.BIR_BRNCode,
                BusFr = entity.BusFr,
                BusTo = entity.BusTo,
                TimeInTimeOutScreen = entity.TimeInTimeOutScreen,
                MilitaryTime = entity.MilitaryTime,
                DecPlaces = entity.DecPlaces,
                WebLogo = entity.WebLogo,
                WebLogoType = entity.WebLogoType,
                WebLogoReports = entity.WebLogoReports,
                WebLogoReportsType = entity.WebLogoReportsType,
                Line1 = entity.Line1,
                Line2 = entity.Line2,
                Head = entity.Head,
                ChartAcct = entity.ChartAcct,
                PayrollPath = entity.PayrollPath,
                HRISPath = entity.HRISPath,
                OTPremiumFlag = entity.OTPremiumFlag,
                TerminalID = entity.TerminalID,
                ValidateLogs = entity.ValidateLogs,
                ReadOnlyTxtDate = entity.ReadOnlyTxtDate,
                Policy = entity.Policy,
                Flag = entity.Flag,
                GSISNo = entity.GSISNo,
                ExportEmail = entity.ExportEmail,
                SiteLogo = entity.SiteLogo,
                SiteContent = entity.SiteContent,
                PasswordHistory = entity.PasswordHistory,
                TKSPhotoPath = entity.TKSPhotoPath,
                ExportLateFilingDateFlag = entity.ExportLateFilingDateFlag,
                EnableAutoPairingLogsFlag = entity.EnableAutoPairingLogsFlag,
                EnableAppOTRawDataFlag = entity.EnableAppOTRawDataFlag,
                Enable2ndShiftRawDataFlag = entity.Enable2ndShiftRawDataFlag
            };
        }
    }
}
