using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface IEmployeeStatusSetUpRepository
    {
        Task<EmployeeStatusSetUp> InsertAsync(EmployeeStatusSetUp employeeStatusSetUp);

        Task<EmployeeStatusSetUp> UpdateAsync(EmployeeStatusSetUp employeeStatusSetUp);

        Task<bool> DeleteAsync(int id);

        Task<EmployeeStatusSetUp?> GetByIdAsync(int id);

        Task<List<EmployeeStatusSetUp>> GetAllAsync();
    }
}
