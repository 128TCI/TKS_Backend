using DomainEntities.DTO.User;
using Infrastructure.IRepositories.UserRepository;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.DTOs.User;
using Services.Interfaces.Authentication;
using Services.DTOs.Encryption; // Ensure this is imported
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Services.DTOs.User.LoginDTO;
using Services.Interfaces.Employee;

namespace Services.Implementation.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _service;
        private readonly IConfiguration _config;
        private readonly IMemoryCache _cache;
        private readonly EncryptionHelper _crypto; // Added EncryptionHelper

        public AuthService(IUserRepository repository, IConfiguration config, IMemoryCache cache, EncryptionHelper crypto, IUserService service)
        {
            _repository = repository;
            _config = config;
            _cache = cache;
            _crypto = crypto;
            _service = service;
        }

        public async Task<bool> IsSuspended(string userName)
        {
            var user = await _repository.GetByUserNameAsync(userName);
            return user?.IsSuspended ?? false;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginDTO loginDto, CancellationToken ct)
        {
            var user = await _repository.GetByUserNameAsync(loginDto.UserName);
            if (user == null) return null;

            var ek = _crypto.GetKey();
            bool isValid = false;

            // 1. Decrypt the password from the database to compare it
            if (!string.IsNullOrEmpty(user.EncryptedPass))
            {
                string decryptedStoredPassword = _crypto.Decrypt(user.EncryptedPass, ek);

                // Compare plain text input with decrypted database password
                isValid = (loginDto.Password == decryptedStoredPassword);
            }

            if (!isValid)
            {
                // Logic for tracking failure count could be added here using user.PasswordFailuresSinceLastSuccess
                return null;
            }

            // Update login status
            user.IsLoggedIn = true;
            user.PasswordFailuresSinceLastSuccess = 0; // Reset failures on success
            await _repository.UpdateAsync(user);

            // Fetch Permissions
            var permissions = await _service.GetPermissionsByUsernameAsync(user.UserName);

            // Cache Permissions
            var permissionKeys = permissions.Select(p => p.PermissionKey).ToList();
            _cache.Set($"Perms_{user.UserID}", permissionKeys, TimeSpan.FromHours(8));

            var userDto = MapToDTO(user);

            return new LoginResponseDTO
            {
                Token = GenerateToken(userDto),
                User = userDto,
                Permissions = permissions
            };
        }

        public async Task<bool> LogoutAsync(int userId, CancellationToken ct)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null) return false;

            user.IsLoggedIn = false;
            await _repository.UpdateAsync(user);

            _cache.Remove($"Perms_{userId}");
            return true;
        }

        private string GenerateToken(UserDTO user)
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
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private UserDTO MapToDTO(UserDTO entity)
        {
            return new UserDTO
            {
                UserID = entity.UserID,
                UserName = entity.UserName,
                Password = null, // Do not send password to client
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
                EncryptedPass = null, // Do not send encrypted pass to client
                LastPasswordFailureDate = entity.LastPasswordFailureDate,
                PasswordFailuresSinceLastSuccess = entity.PasswordFailuresSinceLastSuccess,
                MachineIdentifier = entity.MachineIdentifier
            };
        }
    }
}