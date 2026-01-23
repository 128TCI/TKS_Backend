using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.Maintenance
{
    public interface IEmployeeMasterFileRepository
    {
        Task<EmployeeMasterFile> InsertAsync(EmployeeMasterFile sectionSetUp);

        Task<EmployeeMasterFile> UpdateAsync(EmployeeMasterFile sectionSetUp);

        Task<bool> DeleteAsync(int id);

        Task<EmployeeMasterFile?> GetByIdAsync(int id);

        Task<List<EmployeeMasterFile>> GetAllAsync();
    }
}
