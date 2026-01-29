using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings
{
    public interface IAllowancePerClassificationSetUpRepository
    {
        Task<AllowancePerClassificationSetUpDTO> InsertAsync(AllowancePerClassificationSetUpDTO sectionSetUp);
        Task<AllowancePerClassificationSetUpDTO> UpdateAsync(AllowancePerClassificationSetUpDTO sectionSetUp);
        Task<bool> DeleteAsync(int id);
        Task<AllowancePerClassificationSetUpDTO?> GetByIdAsync(int id);
        Task<List<AllowancePerClassificationSetUpDTO>> GetAllAsync();
    }
}
