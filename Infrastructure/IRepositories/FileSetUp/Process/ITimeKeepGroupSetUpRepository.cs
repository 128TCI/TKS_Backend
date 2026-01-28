using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process
{
    public interface ITimeKeepGroupSetUpRepository
    {
        Task<TimeKeepGroupSetUpDTO> InsertAsync(TimeKeepGroupSetUpDTO timeKeepGroupSetUp);
        Task<TimeKeepGroupSetUpDTO> UpdateAsync(TimeKeepGroupSetUpDTO timeKeepGroupSetUp);
        Task<bool> DeleteAsync(int id);
        Task<TimeKeepGroupSetUpDTO?> GetByIdAsync(int id);
        Task<List<TimeKeepGroupSetUpDTO>> GetAllAsync();
    }
}
