using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface ISectionSetUpRepository
    {
        Task<SectionSetUp> InsertAsync(SectionSetUp sectionSetUp);

        Task<SectionSetUp> UpdateAsync(SectionSetUp sectionSetUp);

        Task<bool> DeleteAsync(int id);

        Task<SectionSetUp?> GetByIdAsync(int id);

        Task<List<SectionSetUp>> GetAllAsync();
    }
}
