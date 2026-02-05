using DomainEntities.DTO;
using DomainEntities.DTO.User;
using Services.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.UserRepository
{
    public interface IUserRepository

    {
        Task<bool> UserNameExistsAsync(string userName);

        Task<UserDTO> InsertAsync(UserDTO employee);

        Task<UserDTO> UpdateAsync(UserDTO employee);

        Task<bool> DeleteAsync(int id);

        Task<UserDTO?> GetByIdAsync(int id);

        Task<List<UserDTO>> GetAllAsync();

        Task<UserDTO?> GetByUserNameAsync(string userName);
        

    }
}
