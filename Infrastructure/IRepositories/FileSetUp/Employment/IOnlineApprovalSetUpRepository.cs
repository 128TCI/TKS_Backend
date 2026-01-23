using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface IOnlineApprovalSetUpRepository
    {
        Task<OnlineApprovalSetUp> InsertAsync(OnlineApprovalSetUp OnlineApprovalSetUp);

        Task<OnlineApprovalSetUp> UpdateAsync(OnlineApprovalSetUp OnlineApprovalSetUp);

        Task<bool> DeleteAsync(int id);

        Task<OnlineApprovalSetUp?> GetByIdAsync(int id);

        Task<List<OnlineApprovalSetUp>> GetAllAsync();
    }
}
