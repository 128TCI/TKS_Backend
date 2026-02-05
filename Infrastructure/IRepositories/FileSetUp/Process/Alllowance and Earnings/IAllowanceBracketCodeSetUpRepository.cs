using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings
{
    public interface IAllowanceBracketCodeSetUpRepository
    {
        Task<AllowanceBracketCodeSetUpDTO> InsertAsync(AllowanceBracketCodeSetUpDTO sectionSetUp);

        Task<AllowanceBracketCodeSetUpDTO> UpdateAsync(AllowanceBracketCodeSetUpDTO sectionSetUp);

        Task<bool> DeleteAsync(int id);

        Task<AllowanceBracketCodeSetUpDTO?> GetByIdAsync(int id);

        Task<List<AllowanceBracketCodeSetUpDTO>> GetAllAsync();
    }
}
