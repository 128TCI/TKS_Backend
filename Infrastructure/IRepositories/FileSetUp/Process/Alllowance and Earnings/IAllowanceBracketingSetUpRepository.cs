using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings
{
    public interface IAllowanceBracketingSetUpRepository
    {
        Task<AllowanceBracketingSetUpDTO> InsertAsync(AllowanceBracketingSetUpDTO allowancePerClassificationSetUp);
        Task<AllowanceBracketingSetUpDTO> UpdateAsync(AllowanceBracketingSetUpDTO allowancePerClassificationSetUp);
        Task<bool> DeleteAsync(int id);
        Task<AllowanceBracketingSetUpDTO?> GetByIdAsync(int id);
        Task<List<AllowanceBracketingSetUpDTO>> GetAllAsync();
    }
}
