using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces.Authentication;
using Services.Interfaces.Encryption;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using static Services.DTOs.User.LoginDTO;
using Services.DTOs.User;

using DomainEntities.DTO.User;
using Infrastructure.IRepositories.UserRepository;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IEncryptionService _encryptionService;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository repository, IEncryptionService encryptionService, IConfiguration config)
    {
        _repository = repository;
        _encryptionService = encryptionService;
        _config = config;
    }
    public async Task<bool> IsSuspended(string userName)
    {
        var user = await _repository.GetByUserNameAsync(userName);
        return user?.IsSuspended ?? false;
    }
    public async Task<LoginResponseDTO?> LoginAsync(LoginDTO loginDto, CancellationToken ct)
    {
        // 1. Decrypt incoming password
        var plainPassword = await _encryptionService.GetCryptoJSDecryptionResultAsync(loginDto.Password, ct);

        // 2. Fetch user
        var users = await _repository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.UserName == loginDto.UserName);

        // 3. Validate credentials
        if (user == null || user.Password != plainPassword)
            return null;

        // 4. Map and Return everything (including IsSuspended status)
        var userDto = MapToDTO(user);

        return new LoginResponseDTO
        {
            Token = GenerateToken(user),
            User = userDto
        };
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"] ?? "Your_Default_Secret_Key_32_Chars_Long");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.EmailAddress ?? "")
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
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