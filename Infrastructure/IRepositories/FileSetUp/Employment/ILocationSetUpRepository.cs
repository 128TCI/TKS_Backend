using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface ILocationSetUpRepository
    {
        Task<LocationSetUp> InsertAsync(LocationSetUp locationSetUp);

        Task<LocationSetUp> UpdateAsync(LocationSetUp locationSetUp);

        Task<bool> DeleteAsync(int id);

        Task<LocationSetUp?> GetByIdAsync(int id);

        Task<List<LocationSetUp>> GetAllAsync();
    }
}
