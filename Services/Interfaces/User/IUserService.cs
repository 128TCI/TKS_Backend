using DomainEntities.DTO.User;
using Services.DTOs.User;

namespace Services.Interfaces.Employee;

public interface IUserService
{
    Task<bool> UserNameExists(string UserName);
    Task<UserDTO> CreateAsync(UserDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
    Task<UserDTO> UpdateAsync(UserDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<UserDTO?> GetByIdAsync(int id);
    Task<List<UserDTO>> GetAllAsync();
    Task<List<UserPermissionDTO>> GetPermissionsByUsernameAsync(string username);
}