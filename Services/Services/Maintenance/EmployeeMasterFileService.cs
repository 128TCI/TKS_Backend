using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.Maintenance;
using Infrastructure.IRepositories.Maintenance;
using Services.DTOs.Encryption;
using Services.DTOs.Maintenance;
using Services.Interfaces.Maintenence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.Maintenance
{
    public class EmployeeMasterFileService : IEmployeeMasterFileService
    {
        private readonly IEmployeeMasterFileRepository _repository;
        private readonly EncryptionHelper _crypto;
        private bool _disposed = false;

        public EmployeeMasterFileService(IEmployeeMasterFileRepository repository, EncryptionHelper crypto)
        {
            _repository = repository;
            _crypto = crypto;
        }

        public async Task<EmployeeMasterFileDTO> CreateAsync(EmployeeMasterFileDTO dto, CancellationToken ct = default)
        {
            var ek = _crypto.GetKey();

            var entity = MapToEntity(dto);
            EncryptEntityFields(entity, ek);

            var result = await _repository.InsertAsync(entity);

            return DecryptDTOFields(MapToDTO(result), ek);
        }

        public async Task<EmployeeMasterFileDTO> UpdateAsync(EmployeeMasterFileDTO dto)
        {
            var ek = _crypto.GetKey();

            var existingEntity = await _repository.GetByIdAsync(dto.EmpID);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Employee with ID {dto.EmpID} not found");

            UpdateEntityFromDTO(existingEntity, dto);
            EncryptEntityFields(existingEntity, ek);

            var result = await _repository.UpdateAsync(existingEntity);

            return DecryptDTOFields(MapToDTO(result), ek);
        }

        public async Task<EmployeeMasterFileDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            var ek = _crypto.GetKey();
            return DecryptDTOFields(MapToDTO(entity), ek);
        }

        public async Task<List<EmployeeMasterFileDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            if (users == null || !users.Any()) return new List<EmployeeMasterFileDTO>();

            var ek = _crypto.GetKey();

            // Map and Decrypt each item
            return users.Select(user => DecryptDTOFields(MapToDTO(user), ek)).ToList();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        // --- Test Method for Encryption Verification ---
        public async Task<object> TestEncryptionAsync(int empId)
        {
            var employee = await _repository.GetByIdAsync(empId);
            if (employee == null)
                return new { success = false, message = $"Employee with ID {empId} not found" };

            var ek = _crypto.GetKey();

            // Test decryption of existing encrypted data
            var decryptedData = new
            {
                lName = _crypto.Decrypt(employee.LName ?? "", ek),
                fName = _crypto.Decrypt(employee.FName ?? "", ek),
                mName = _crypto.Decrypt(employee.MName ?? "", ek),
                sssNo = _crypto.Decrypt(employee.SSSNo ?? "", ek),
                philHealthNo = _crypto.Decrypt(employee.PHilHealthNo ?? "", ek),
                pagIbigNo = _crypto.Decrypt(employee.PagIbigNo ?? "", ek),
                tin = _crypto.Decrypt(employee.TIN ?? "", ek),
                gsisNo = _crypto.Decrypt(employee.GSISNo ?? "", ek),
                hAddress = _crypto.Decrypt(employee.HAddress ?? "", ek),
                city = _crypto.Decrypt(employee.City ?? "", ek),
                mobilePhone = _crypto.Decrypt(employee.MobilePhone ?? "", ek)
            };

            // Test round-trip encryption
            var roundTripTest = new
            {
                originalEncrypted = employee.LName,
                decrypted = decryptedData.lName,
                reEncrypted = _crypto.Encrypt(decryptedData.lName ?? "", ek),
                matches = _crypto.Encrypt(decryptedData.lName ?? "", ek) == employee.LName
            };

            return new
            {
                success = true,
                empCode = employee.EmpCode,
                email = employee.Email,
                encryptedInDatabase = new
                {
                    lName = employee.LName,
                    fName = employee.FName,
                    mName = employee.MName,
                    sssNo = employee.SSSNo,
                    philHealthNo = employee.PHilHealthNo,
                    pagIbigNo = employee.PagIbigNo,
                    tin = employee.TIN,
                    gsisNo = employee.GSISNo
                },
                decryptedValues = decryptedData,
                roundTripTest = roundTripTest,
                keyInfo = new
                {
                    secretKey = ek.SecretKey,
                    salt = ek.Salt,
                    keySize = ek.KeySize,
                    withEncryption = ek.WithEncryption
                }
            };
        }

        // Test with specific encrypted strings
        public object TestDecryptSpecificData()
        {
            var ek = _crypto.GetKey();

            // Your actual encrypted data from the database
            var testCases = new[]
            {
                new { field = "Last Name", encrypted = "fmj2yPK2a0StNXzCqbPsQQ==" },
                new { field = "First Name", encrypted = "NsAOrj8HxCkeokWLhzYxPg==" },
                new { field = "Middle Name", encrypted = "2vD2W5NFctBh8uDWAn1jfw==" },
                new { field = "SSS", encrypted = "XrdYaK5oM9NUbPhk+n2hpuoPP0QTrcZZIiDfHs6EmhQ=" },
                new { field = "PhilHealth", encrypted = "XrdYaK5oM9NUbPhk+n2hpssn//Qsp2acmWAA0ZxW/80=" },
                new { field = "Pag-IBIG", encrypted = "XrdYaK5oM9NUbPhk+n2hpssn//Qsp2acmWAA0ZxW/80=" },
                new { field = "TIN", encrypted = "PGAd/Ee9wKuJLPyceLrED84qv65jM5sa87Z9+7Z6g18=" },
                new { field = "GSIS", encrypted = "XrdYaK5oM9NUbPhk+n2hpgVhuAEyaBlBDxMmOCe5QO8=" }
            };

            var results = testCases.Select(test => new
            {
                field = test.field,
                encrypted = test.encrypted,
                decrypted = _crypto.Decrypt(test.encrypted, ek),
                isError = _crypto.Decrypt(test.encrypted, ek) == "ERROR" ||
                          _crypto.Decrypt(test.encrypted, ek) == test.encrypted
            }).ToList();

            // Test round-trip with the first item
            var firstDecrypted = _crypto.Decrypt(testCases[0].encrypted, ek);
            var reEncrypted = _crypto.Encrypt(firstDecrypted, ek);

            return new
            {
                decryptionResults = results,
                roundTripTest = new
                {
                    field = testCases[0].field,
                    originalEncrypted = testCases[0].encrypted,
                    decrypted = firstDecrypted,
                    reEncrypted = reEncrypted,
                    matches = testCases[0].encrypted == reEncrypted
                },
                summary = new
                {
                    totalTests = testCases.Length,
                    successfulDecryptions = results.Count(r => !r.isError),
                    failedDecryptions = results.Count(r => r.isError)
                }
            };
        }

        // --- Encryption/Decryption Methods ---

        private void EncryptEntityFields(EmployeeMasterFile entity, EncryptionKeyUpdated ek)
        {
            // Personal Information
            entity.LName = _crypto.Encrypt(entity.LName ?? string.Empty, ek);
            entity.FName = _crypto.Encrypt(entity.FName ?? string.Empty, ek);
            entity.MName = _crypto.Encrypt(entity.MName ?? string.Empty, ek);
            entity.Suffix = _crypto.Encrypt(entity.Suffix ?? string.Empty, ek);

            // Government IDs
            entity.SSSNo = _crypto.Encrypt(entity.SSSNo ?? string.Empty, ek);
            entity.PHilHealthNo = _crypto.Encrypt(entity.PHilHealthNo ?? string.Empty, ek);
            entity.PagIbigNo = _crypto.Encrypt(entity.PagIbigNo ?? string.Empty, ek);
            entity.TIN = _crypto.Encrypt(entity.TIN ?? string.Empty, ek);
            entity.GSISNo = _crypto.Encrypt(entity.GSISNo ?? string.Empty, ek);

            // Address Information
            entity.HAddress = _crypto.Encrypt(entity.HAddress ?? string.Empty, ek);
            entity.PAddress = _crypto.Encrypt(entity.PAddress ?? string.Empty, ek);
            entity.City = _crypto.Encrypt(entity.City ?? string.Empty, ek);
            entity.Province = _crypto.Encrypt(entity.Province ?? string.Empty, ek);
            entity.PostalCode = _crypto.Encrypt(entity.PostalCode ?? string.Empty, ek);

            // Contact Information
            entity.MobilePhone = _crypto.Encrypt(entity.MobilePhone ?? string.Empty, ek);
            entity.HomePhone = _crypto.Encrypt(entity.HomePhone ?? string.Empty, ek);
            entity.PresentPhone = _crypto.Encrypt(entity.PresentPhone ?? string.Empty, ek);

            // Other Personal Data
            entity.BirthPlace = _crypto.Encrypt(entity.BirthPlace ?? string.Empty, ek);
            entity.Citizenship = _crypto.Encrypt(entity.Citizenship ?? string.Empty, ek);
            entity.CivilStatus = _crypto.Encrypt(entity.CivilStatus ?? string.Empty, ek);
        }

        private EmployeeMasterFileDTO DecryptDTOFields(EmployeeMasterFileDTO dto, EncryptionKeyUpdated ek)
        {
            // Personal Information
            dto.LName = _crypto.Decrypt(dto.LName ?? string.Empty, ek);
            dto.FName = _crypto.Decrypt(dto.FName ?? string.Empty, ek);
            dto.MName = _crypto.Decrypt(dto.MName ?? string.Empty, ek);
            dto.Suffix = _crypto.Decrypt(dto.Suffix ?? string.Empty, ek);

            // Government IDs
            dto.SSSNo = _crypto.Decrypt(dto.SSSNo ?? string.Empty, ek);
            dto.PHilHealthNo = _crypto.Decrypt(dto.PHilHealthNo ?? string.Empty, ek);
            dto.PagIbigNo = _crypto.Decrypt(dto.PagIbigNo ?? string.Empty, ek);
            dto.TIN = _crypto.Decrypt(dto.TIN ?? string.Empty, ek);
            dto.GSISNo = _crypto.Decrypt(dto.GSISNo ?? string.Empty, ek);

            // Address Information
            dto.HAddress = _crypto.Decrypt(dto.HAddress ?? string.Empty, ek);
            dto.PAddress = _crypto.Decrypt(dto.PAddress ?? string.Empty, ek);
            dto.City = _crypto.Decrypt(dto.City ?? string.Empty, ek);
            dto.Province = _crypto.Decrypt(dto.Province ?? string.Empty, ek);
            dto.PostalCode = _crypto.Decrypt(dto.PostalCode ?? string.Empty, ek);

            // Contact Information
            dto.MobilePhone = _crypto.Decrypt(dto.MobilePhone ?? string.Empty, ek);
            dto.HomePhone = _crypto.Decrypt(dto.HomePhone ?? string.Empty, ek);
            dto.PresentPhone = _crypto.Decrypt(dto.PresentPhone ?? string.Empty, ek);

            // Other Personal Data
            dto.BirthPlace = _crypto.Decrypt(dto.BirthPlace ?? string.Empty, ek);
            dto.Citizenship = _crypto.Decrypt(dto.Citizenship ?? string.Empty, ek);
            dto.CivilStatus = _crypto.Decrypt(dto.CivilStatus ?? string.Empty, ek);

            return dto;
        }

        private void UpdateEntityFromDTO(EmployeeMasterFile entity, EmployeeMasterFileDTO dto)
        {
            // Employment Information
            entity.EmpCode = dto.EmpCode;
            entity.EmpStatCode = dto.EmpStatCode;
            entity.Courtesy = dto.Courtesy;

            // Personal Information (will be encrypted)
            entity.LName = dto.LName;
            entity.FName = dto.FName;
            entity.MName = dto.MName;
            entity.Suffix = dto.Suffix;
            entity.NickName = dto.NickName;
            entity.Sex = dto.Sex;
            entity.Email = dto.Email;

            // Dates
            entity.BirthDate = dto.BirthDate;
            entity.DateHired = dto.DateHired;
            entity.DateRegularized = dto.DateRegularized;
            entity.DateResigned = dto.DateResigned;
            entity.DateSuspended = dto.DateSuspended;
            entity.ProbeStart = dto.ProbeStart;
            entity.ProbeEnd = dto.ProbeEnd;

            // Government IDs (will be encrypted)
            entity.SSSNo = dto.SSSNo;
            entity.PHilHealthNo = dto.PHilHealthNo;
            entity.PagIbigNo = dto.PagIbigNo;
            entity.TIN = dto.TIN;
            entity.GSISNo = dto.GSISNo;

            // Address (will be encrypted)
            entity.HAddress = dto.HAddress;
            entity.PAddress = dto.PAddress;
            entity.City = dto.City;
            entity.Province = dto.Province;
            entity.PostalCode = dto.PostalCode;

            // Contact (will be encrypted)
            entity.MobilePhone = dto.MobilePhone;
            entity.HomePhone = dto.HomePhone;
            entity.PresentPhone = dto.PresentPhone;

            // Other Personal (will be encrypted)
            entity.BirthPlace = dto.BirthPlace;
            entity.Citizenship = dto.Citizenship;
            entity.CivilStatus = dto.CivilStatus;
            entity.Religion = dto.Religion;

            // Physical
            entity.Weight = dto.Weight;
            entity.Height = dto.Height;
            entity.Age = dto.Age;

            // Employment Status
            entity.Suspend = dto.Suspend;
            entity.Separated = dto.Separated;
            entity.UnionMember = dto.UnionMember;
            entity.Agency = dto.Agency;
            entity.Contractual = dto.Contractual;

            // Organizational Structure
            entity.DivCode = dto.DivCode;
            entity.DepCode = dto.DepCode;
            entity.SecCode = dto.SecCode;
            entity.GrpCode = dto.GrpCode;
            entity.BraCode = dto.BraCode;
            entity.UnitCode = dto.UnitCode;
            entity.AreaCode = dto.AreaCode;
            entity.LocCode = dto.LocCode;

            // Job Information
            entity.DesCode = dto.DesCode;
            entity.ShiftCode = dto.ShiftCode;
            entity.Superior = dto.Superior;
            entity.GrdCode = dto.GrdCode;
            entity.ClsCode = dto.ClsCode;
            entity.CatCode = dto.CatCode;

            // Compensation
            entity.PayCode = dto.PayCode;
            entity.RateCode = dto.RateCode;
            entity.SubAcctCode = dto.SubAcctCode;

            // Tax and Banking
            entity.TaxID = dto.TaxID;
            entity.TaxCode = dto.TaxCode;
            entity.BankAccount = dto.BankAccount;
            entity.BankCode = dto.BankCode;
            entity.PagibigCode = dto.PagibigCode;

            // Miscellaneous
            entity.LocId = dto.LocId;
            entity.OnlineAppCode = dto.OnlineAppCode;
            entity.photo = dto.photo;
        }

        // --- Mapping Methods (Unchanged from your original) ---

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