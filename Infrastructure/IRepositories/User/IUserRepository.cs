using DomainEntities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntities.DTO.User;

namespace Infrastructure.IRepositories.UserRepository
{
    public interface IUserRepository

    {
        Task<bool> UserNameExistsAsync(string userName);

        Task<User> InsertAsync(User employee);

        Task<User> UpdateAsync(User employee);

        Task<bool> DeleteAsync(int id);

        Task<User?> GetByIdAsync(int id);

        Task<List<User>> GetAllAsync();

        Task<User?> GetByUserNameAsync(string userName);

    }
}
