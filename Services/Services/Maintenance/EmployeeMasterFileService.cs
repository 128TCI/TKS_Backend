using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.Maintenance;
using Infrastructure.IRepositories.FileSetUp.Employment;
using Infrastructure.IRepositories.Maintenance;
using Services.DTOs.FileSetup.Employment;
using Services.DTOs.Maintenance;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Employment;
using Services.Interfaces.Maintenence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Maintenance
{
    public class EmployeeMasterFileService : IEmployeeMasterFileService
    {
        private readonly IEmployeeMasterFileRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public EmployeeMasterFileService(IEmployeeMasterFileRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<EmployeeMasterFileDTO> CreateAsync(EmployeeMasterFileDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<EmployeeMasterFileDTO> UpdateAsync(EmployeeMasterFileDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<EmployeeMasterFileDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<EmployeeMasterFileDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<EmployeeMasterFileDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private EmployeeMasterFile MapToEntity(EmployeeMasterFileDTO dto)
        {

            return new EmployeeMasterFile
            {
                EmpID = dto.EmpID,
                EmpCode = dto.EmpCode,
                EmpStatCode = dto.EmpStatCode,
                Courtesy = dto.Courtesy,
                LName = dto.LName,
                FName = dto.FName,
                MName = dto.MName,
                NickName = dto.NickName,
                HAddress = dto.HAddress,
                PAddress = dto.PAddress,
                City = dto.City,
                Province = dto.Province,
                PostalCode = dto.PostalCode,
                CivilStatus = dto.CivilStatus,
                Citizenship = dto.Citizenship,
                Religion = dto.Religion,
                Sex = dto.Sex,
                Email = dto.Email,
                Weight = dto.Weight,
                Height = dto.Height,
                MobilePhone = dto.MobilePhone,
                HomePhone = dto.HomePhone,
                PresentPhone = dto.PresentPhone,
                DateHired = dto.DateHired,
                DateRegularized = dto.DateRegularized,
                DateResigned = dto.DateResigned,
                DateSuspended = dto.DateSuspended,
                ProbeStart = dto.ProbeStart,
                ProbeEnd = dto.ProbeEnd,
                Suspend = dto.Suspend,
                Separated = dto.Separated,
                BirthDate = dto.BirthDate,
                Age = dto.Age,
                BirthPlace = dto.BirthPlace,
                UnionMember = dto.UnionMember,
                Agency = dto.Agency,
                DivCode = dto.DivCode,
                DepCode = dto.DepCode,
                SecCode = dto.SecCode,
                GrpCode = dto.GrpCode,
                BraCode = dto.BraCode,
                SubAcctCode = dto.SubAcctCode,
                DesCode = dto.DesCode,
                ShiftCode = dto.ShiftCode,
                Superior = dto.Superior,
                GrdCode = dto.GrdCode,
                ClsCode = dto.ClsCode,
                PayCode = dto.PayCode,
                LocId = dto.LocId,
                RateCode = dto.RateCode,
                TaxID = dto.TaxID,
                TaxCode = dto.TaxCode,
                BankAccount = dto.BankAccount,
                BankCode = dto.BankCode,
                SSSNo = dto.SSSNo,
                PHilHealthNo = dto.PHilHealthNo,
                PagIbigNo = dto.PagIbigNo,
                TIN = dto.TIN,
                PagibigCode = dto.PagibigCode,
                photo = dto.photo,
                CatCode = dto.CatCode,
                UnitCode = dto.UnitCode,
                Contractual = dto.Contractual,
                AreaCode = dto.AreaCode,
                LocCode = dto.LocCode,
                GSISNo = dto.GSISNo,
                Suffix = dto.Suffix,
                OnlineAppCode = dto.OnlineAppCode
            };
        }

        private EmployeeMasterFileDTO MapToDTO(EmployeeMasterFile entity)
        {
            return new EmployeeMasterFileDTO
            {
                EmpID = entity.EmpID,
                EmpCode = entity.EmpCode,
                EmpStatCode = entity.EmpStatCode,
                Courtesy = entity.Courtesy,
                LName = entity.LName,
                FName = entity.FName,
                MName = entity.MName,
                NickName = entity.NickName,
                HAddress = entity.HAddress,
                PAddress = entity.PAddress,
                City = entity.City,
                Province = entity.Province,
                PostalCode = entity.PostalCode,
                CivilStatus = entity.CivilStatus,
                Citizenship = entity.Citizenship,
                Religion = entity.Religion,
                Sex = entity.Sex,
                Email = entity.Email,
                Weight = entity.Weight,
                Height = entity.Height,
                MobilePhone = entity.MobilePhone,
                HomePhone = entity.HomePhone,
                PresentPhone = entity.PresentPhone,
                DateHired = entity.DateHired,
                DateRegularized = entity.DateRegularized,
                DateResigned = entity.DateResigned,
                DateSuspended = entity.DateSuspended,
                ProbeStart = entity.ProbeStart,
                ProbeEnd = entity.ProbeEnd,
                Suspend = entity.Suspend,
                Separated = entity.Separated,
                BirthDate = entity.BirthDate,
                Age = entity.Age,
                BirthPlace = entity.BirthPlace,
                UnionMember = entity.UnionMember,
                Agency = entity.Agency,
                DivCode = entity.DivCode,
                DepCode = entity.DepCode,
                SecCode = entity.SecCode,
                GrpCode = entity.GrpCode,
                BraCode = entity.BraCode,
                SubAcctCode = entity.SubAcctCode,
                DesCode = entity.DesCode,
                ShiftCode = entity.ShiftCode,
                Superior = entity.Superior,
                GrdCode = entity.GrdCode,
                ClsCode = entity.ClsCode,
                PayCode = entity.PayCode,
                LocId = entity.LocId,
                RateCode = entity.RateCode,
                TaxID = entity.TaxID,
                TaxCode = entity.TaxCode,
                BankAccount = entity.BankAccount,
                BankCode = entity.BankCode,
                SSSNo = entity.SSSNo,
                PHilHealthNo = entity.PHilHealthNo,
                PagIbigNo = entity.PagIbigNo,
                TIN = entity.TIN,
                PagibigCode = entity.PagibigCode,
                photo = entity.photo,
                CatCode = entity.CatCode,
                UnitCode = entity.UnitCode,
                Contractual = entity.Contractual,
                AreaCode = entity.AreaCode,
                LocCode = entity.LocCode,
                GSISNo = entity.GSISNo,
                Suffix = entity.Suffix,
                OnlineAppCode = entity.OnlineAppCode
            };
        }
    }
}
