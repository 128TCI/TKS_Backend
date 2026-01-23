using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface IUnitSetUpRepository
    {
        Task<UnitSetUp> InsertAsync(UnitSetUp sectionSetUp);

        Task<UnitSetUp> UpdateAsync(UnitSetUp sectionSetUp);

        Task<bool> DeleteAsync(int id);

        Task<UnitSetUp?> GetByIdAsync(int id);

        Task<List<UnitSetUp>> GetAllAsync();
    }
}
