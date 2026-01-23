using DomainEntities.DTO;
using System.Security.Cryptography;
using System.Text;
using Services.Interfaces.Encryption;
using Services.Interfaces.Employee;
using Services.DTOs.User;
using Infrastructure.IRepositories.UserRepository;
using DomainEntities.DTO.User;

namespace Services.Services.UserRepository;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IEncryptionService _encryptionService; // Injected service

    public UserService(IUserRepository repository, IEncryptionService encryptionService)
    {
        _repository = repository;
        _encryptionService = encryptionService;
    }

    public async Task<bool> UserNameExists(string userName)
    {
        // Note: You should add this method to your IEmployeeRepository interface
        // to keep the service decoupled from the DB context.
        return await _repository.UserNameExistsAsync(userName);
    }

    public async Task<UserDTO> CreateAsync(UserDTO dto, CancellationToken ct = default)
    {
        // 1. Initialize the decryption task
        // This takes the encrypted string from the DTO and converts it to a Task
        Task<string> TaskPassword = _encryptionService.GetCryptoJSDecryptionResultAsync(dto.Password, ct).AsTask();

        // 2. Await the task to get the plain text password
        string plainPassword = await TaskPassword;

        // 3. Map to Entity
        var user = MapToEntity(dto);

        // 4. Assign the decrypted password to the entity
        // This replaces the encrypted string with the plain text version
        user.Password = plainPassword;

        // 5. Set Audit fields
        user.CreatedDate = DateTime.Now;

        // 6. Save to Database
        var result = await _repository.InsertAsync(user);

        return MapToDTO(result);
    }

    public async Task<UserDTO> UpdateAsync(UserDTO dto)
    {
        var user = MapToEntity(dto);

        // Auto-set edit timestamp
        user.EditedDate = DateTime.Now;

        var result = await _repository.UpdateAsync(user);
        return MapToDTO(result);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<UserDTO?> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        return user == null ? null : MapToDTO(user);
    }

    public async Task<List<UserDTO>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();

        if (users == null || !users.Any())
        {
            return new List<UserDTO>(); // Return empty list instead of null
        }

        return users.Select(MapToDTO).ToList();
    }

    // --- Mapping Logic ---

    private User MapToEntity(UserDTO dto)
    {
        return new User
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

    private UserDTO MapToDTO(User entity)
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