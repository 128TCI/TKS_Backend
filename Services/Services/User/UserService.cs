using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using DomainEntities.DTO.User;
using Infrastructure.IRepositories.UserRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Services.DTOs.Encryption;
using Services.DTOs.User;
using Services.Interfaces.Employee;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.UserRepository;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly EncryptionHelper _crypto;
    private readonly string _connectionString;
    public UserService(IUserRepository repository, EncryptionHelper crypto, IConfiguration config)
    {
        _repository = repository;
        _crypto = crypto;
        _connectionString = config.GetConnectionString("TKS")
            ?? throw new InvalidOperationException("Connection string 'TKS' not found.");
    }

    public async Task<bool> UserNameExists(string userName)
    {
        return await _repository.UserNameExistsAsync(userName);
    }

    public async Task<UserDTO> CreateAsync(UserDTO dto, CancellationToken ct = default)
    {
        var ek = _crypto.GetKey();
        var entity = MapToEntity(dto);

        // Encrypt sensitive fields
        if (!string.IsNullOrEmpty(entity.Password))
            entity.Password = _crypto.Encrypt(entity.Password, ek);

        if (!string.IsNullOrEmpty(entity.EncryptedPass))
            entity.EncryptedPass = _crypto.Encrypt(entity.EncryptedPass, ek);

        entity.CreatedDate = DateTime.Now;

        var result = await _repository.InsertAsync(entity);
        return DecryptDTOFields(MapToDTO(result), ek);
    }

    public async Task<UserDTO> UpdateAsync(UserDTO dto)
    {
        var ek = _crypto.GetKey();
        var existingUser = await _repository.GetByIdAsync(dto.UserID);

        if (existingUser == null)
            throw new KeyNotFoundException("User not found");

        // Map all data including audit and failure tracking
        UpdateEntityFromDTO(existingUser, dto);

        // Encrypt updated password if provided
        if (!string.IsNullOrEmpty(dto.Password))
            existingUser.Password = _crypto.Encrypt(dto.Password, ek);

        existingUser.EditedDate = DateTime.Now;

        var result = await _repository.UpdateAsync(existingUser);
        return DecryptDTOFields(MapToDTO(result), ek);
    }

    public async Task<UserDTO?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;

        var ek = _crypto.GetKey();
        return DecryptDTOFields(MapToDTO(entity), ek);
    }

    public async Task<List<UserDTO>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        if (users == null || !users.Any()) return new List<UserDTO>();

        var ek = _crypto.GetKey();
        return users.Select(user => DecryptDTOFields(MapToDTO(user), ek)).ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    // --- Encryption/Decryption Helpers ---

    private UserDTO DecryptDTOFields(UserDTO dto, EncryptionKeyUpdated ek)
    {
        if (!string.IsNullOrEmpty(dto.Password))
            dto.Password = _crypto.Decrypt(dto.Password, ek);

        if (!string.IsNullOrEmpty(dto.EncryptedPass))
            dto.EncryptedPass = _crypto.Decrypt(dto.EncryptedPass, ek);

        return dto;
    }

    private void UpdateEntityFromDTO(UserDTO entity, UserDTO dto)
    {
        entity.UserName = dto.UserName ?? string.Empty;
        entity.ExpirationDate = dto.ExpirationDate;
        entity.IsLoggedIn = dto.IsLoggedIn;
        entity.IsSuspended = dto.IsSuspended;
        entity.MachineName = dto.MachineName ?? string.Empty;
        entity.EditedBy = dto.EditedBy;
        entity.EmpCode = dto.EmpCode;
        entity.IsWindowsAuthenticate = dto.IsWindowsAuthenticate;
        entity.WindowsLoginName = dto.WindowsLoginName;
        entity.EmailAddress = dto.EmailAddress;
        entity.ForgotPasswordExpiry = dto.ForgotPasswordExpiry;
        entity.ForgotPasswordCode = dto.ForgotPasswordCode;
        entity.LastPasswordFailureDate = dto.LastPasswordFailureDate;
        entity.PasswordFailuresSinceLastSuccess = dto.PasswordFailuresSinceLastSuccess;
        entity.MachineIdentifier = dto.MachineIdentifier;
        // Password and EncryptedPass are handled in UpdateAsync to ensure encryption
    }
    public async Task<List<UserPermissionDTO>> GetPermissionsByUsernameAsync(string username)
    {
        // 1. Get the encryption key from your helper
        var ek = _crypto.GetKey();

        using (var connection = new SqlConnection(_connectionString))
        {
            // 2. Fetch from database using Dapper
            var results = await connection.QueryAsync<UserPermissionDTO>(
                "sp_tk_GetAllUserPermissions",
                new { Username = username },
                commandType: CommandType.StoredProcedure
            );

            var permissionList = results.ToList();

            // 3. Encrypt values using the retrieved key
            foreach (var permission in permissionList)
            {
                if (!string.IsNullOrEmpty(permission.PermissionKey))
                {
                    // Now 'ek' is defined and accessible
                    permission.PermissionKey = _crypto.Encrypt(permission.PermissionKey, ek);
                    permission.AccessTypeName = _crypto.Encrypt(permission.AccessTypeName, ek);
                    permission.FormName = _crypto.Encrypt(permission.FormName, ek); 
                }
            }

            return permissionList;
        }
    }

    // --- Mapping Logic (All fields preserved) ---

    private UserDTO MapToEntity(UserDTO dto)
    {
        return new UserDTO
        {
            UserID = dto.UserID,
            UserName = dto.UserName ?? string.Empty,
            Password = dto.Password,
            ExpirationDate = dto.ExpirationDate,
            IsLoggedIn = dto.IsLoggedIn,
            IsSuspended = dto.IsSuspended,
            MachineName = dto.MachineName ?? string.Empty,
            CreatedBy = dto.CreatedBy,
            CreatedDate = dto.CreatedDate,
            EditedBy = dto.EditedBy,
            EditedDate = dto.EditedDate,
            EmpCode = dto.EmpCode,
            IsWindowsAuthenticate = dto.IsWindowsAuthenticate,
            WindowsLoginName = dto.WindowsLoginName,
            EmailAddress = dto.EmailAddress,
            ForgotPasswordExpiry = dto.ForgotPasswordExpiry,
            ForgotPasswordCode = dto.ForgotPasswordCode,
            EncryptedPass = dto.EncryptedPass,
            LastPasswordFailureDate = dto.LastPasswordFailureDate,
            PasswordFailuresSinceLastSuccess = dto.PasswordFailuresSinceLastSuccess,
            MachineIdentifier = dto.MachineIdentifier
        };
    }

    private UserDTO MapToDTO(UserDTO entity)
    {
        return new UserDTO
        {
            UserID = entity.UserID,
            UserName = entity.UserName,
            Password = entity.Password,
            ExpirationDate = entity.ExpirationDate,
            IsLoggedIn = entity.IsLoggedIn,
            IsSuspended = entity.IsSuspended,
            MachineName = entity.MachineName,
            CreatedBy = entity.CreatedBy,
            CreatedDate = entity.CreatedDate,
            EditedBy = entity.EditedBy,
            EditedDate = entity.EditedDate,
            EmpCode = entity.EmpCode,
            IsWindowsAuthenticate = entity.IsWindowsAuthenticate,
            WindowsLoginName = entity.WindowsLoginName,
            EmailAddress = entity.EmailAddress,
            ForgotPasswordExpiry = entity.ForgotPasswordExpiry,
            ForgotPasswordCode = entity.ForgotPasswordCode,
            EncryptedPass = entity.EncryptedPass,
            LastPasswordFailureDate = entity.LastPasswordFailureDate,
            PasswordFailuresSinceLastSuccess = entity.PasswordFailuresSinceLastSuccess,
            MachineIdentifier = entity.MachineIdentifier
        };
    }
}