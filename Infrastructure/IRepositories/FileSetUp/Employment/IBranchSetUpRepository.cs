using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface IBranchSetUpRepository
    {
        Task<BranchSetUp> InsertAsync(BranchSetUp branchSetUp);

        Task<BranchSetUp> UpdateAsync(BranchSetUp branchSetUp);

        Task<bool> DeleteAsync(int id);

        Task<BranchSetUp?> GetByIdAsync(int id);

        Task<List<BranchSetUp>> GetAllAsync();
    }
}
