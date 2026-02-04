using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device
{
    public interface IDTRFlagSetUpRepository
    {
        Task<DTRFlagSetUpDTO> InsertAsync(DTRFlagSetUpDTO data);
        Task<DTRFlagSetUpDTO> UpdateAsync(DTRFlagSetUpDTO data);
        Task<bool> DeleteAsync(int id);
        Task<DTRFlagSetUpDTO?> GetByIdAsync(int id);
        Task<List<DTRFlagSetUpDTO>> GetAllAsync();
    }
}
