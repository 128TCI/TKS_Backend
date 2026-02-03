using DomainEntities.DTO.User;

namespace Services.DTOs.User // Recommended to keep DTOs in the Services layer
{
    public class LoginDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Nested class to match IAuthService return type
        public class LoginResponseDTO
        {
            public string Token { get; set; } = string.Empty;

            // FIX: Change 'User' (Entity) to 'UserDTO' (Safe Data Object)
            public UserDTO User { get; set; } = new();

            public List<UserPermissionDTO> Permissions { get; set; } = new();
        }
    }

    public class UserPermissionDTO
    {
        public string FormName { get; set; } = string.Empty;
        public string AccessTypeName { get; set; } = string.Empty;
        public string PermissionKey { get; set; } = string.Empty;
    }

    public class LogoutDTO
    {
        public int UserId { get; set; }
    }
}