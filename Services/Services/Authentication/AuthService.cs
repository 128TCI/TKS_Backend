using DomainEntities.DTO.User;
using Infrastructure.IRepositories.UserRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.DTOs.User;
using Services.Interfaces.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Services.DTOs.User.LoginDTO;
using BCrypt.Net;

namespace Services.Implementation.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<bool> IsSuspended(string userName)
        {
            var user = await _repository.GetByUserNameAsync(userName);
            return user?.IsSuspended ?? false;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginDTO loginDto, CancellationToken ct)
        {
            // 1. Fetch user by username
            var user = await _repository.GetByUserNameAsync(loginDto.UserName);

            // 2. Check if user exists
            if (user == null)
                return null;

            // 3. Check if password is hashed or plain text and validate accordingly
            bool isValid = false;

            // Check if password is already hashed (BCrypt hashes start with $2)
            if (user.Password.StartsWith("$2"))
            {
                // Verify hashed password - USE FULL NAMESPACE
                isValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            }
            else
            {
                // Plain text password - verify and migrate to BCrypt
                isValid = user.Password == loginDto.Password;

                if (isValid)
                {
                    // Hash the password for future logins - USE FULL NAMESPACE
                    user.Password = BCrypt.Net.BCrypt.HashPassword(loginDto.Password, BCrypt.Net.BCrypt.GenerateSalt(12));
                }
            }

            if (!isValid)
                return null;

            // 4. Update IsLoggedIn status to true
            user.IsLoggedIn = true;
            await _repository.UpdateAsync(user);

            // 5. Map and Return everything
            var userDto = MapToDTO(user);

            return new LoginResponseDTO
            {
                Token = GenerateToken(user),
                User = userDto
            };
        }

        public async Task<bool> LogoutAsync(int userId, CancellationToken ct)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (user == null)
                return false;

            user.IsLoggedIn = false;
            await _repository.UpdateAsync(user);

            return true;
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
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
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
                Password = null,
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
                EncryptedPass = null,
                LastPasswordFailureDate = entity.LastPasswordFailureDate,
                PasswordFailuresSinceLastSuccess = entity.PasswordFailuresSinceLastSuccess,
                MachineIdentifier = entity.MachineIdentifier
            };
        }

        // Helper method to hash passwords when creating new users
        public string HashPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword, BCrypt.Net.BCrypt.GenerateSalt(12));
        }
    }
}