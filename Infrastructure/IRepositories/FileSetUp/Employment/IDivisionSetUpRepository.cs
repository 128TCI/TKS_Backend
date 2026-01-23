using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface IDivisionSetUpRepository
    {
        Task<DivisionSetUp> InsertAsync(DivisionSetUp divisionSetUp);

        Task<DivisionSetUp> UpdateAsync(DivisionSetUp divisionSetUp);

        Task<bool> DeleteAsync(int id);

        Task<DivisionSetUp?> GetByIdAsync(int id);

        Task<List<DivisionSetUp>> GetAllAsync();
    }
}
