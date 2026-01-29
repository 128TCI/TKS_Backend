using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device
{
    public interface IAMSDbConfigSetUpRepository
    {
        Task<AMSDbConfigSetUpDTO> InsertAsync(AMSDbConfigSetUpDTO calendarSetUp);
        Task<AMSDbConfigSetUpDTO> UpdateAsync(AMSDbConfigSetUpDTO calendarSetUp);
        Task<bool> DeleteAsync(int id);
        Task<AMSDbConfigSetUpDTO?> GetByIdAsync(int id);
        Task<List<AMSDbConfigSetUpDTO>> GetAllAsync();
    }
}
