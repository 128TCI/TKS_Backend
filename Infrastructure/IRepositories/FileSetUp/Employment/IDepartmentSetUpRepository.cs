using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntities.DTO.FileSetUp.Employment;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface IDepartmentSetUpRepository
    {
        Task<DepartmentSetUp> InsertAsync(DepartmentSetUp departmentSetUp);

        Task<DepartmentSetUp> UpdateAsync(DepartmentSetUp departmentSetUp);

        Task<bool> DeleteAsync(int id);

        Task<DepartmentSetUp?> GetByIdAsync(int id);

        Task<List<DepartmentSetUp>> GetAllAsync();
    }
}
