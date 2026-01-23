using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface IAreaSetUpRepository
    {
        Task<AreaSetUp> InsertAsync(AreaSetUp areaSetUp);

        Task<AreaSetUp> UpdateAsync(AreaSetUp areaSetUp);

        Task<bool> DeleteAsync(int id);

        Task<AreaSetUp?> GetByIdAsync(int id);

        Task<List<AreaSetUp>> GetAllAsync();
    }
}
