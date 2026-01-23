using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface IGroupSetUpRepository
    {
        Task<GroupSetUp> InsertAsync(GroupSetUp groupSetUp);

        Task<GroupSetUp> UpdateAsync(GroupSetUp groupSetUp);

        Task<bool> DeleteAsync(int id);

        Task<GroupSetUp?> GetByIdAsync(int id);

        Task<List<GroupSetUp>> GetAllAsync();
    }
}
