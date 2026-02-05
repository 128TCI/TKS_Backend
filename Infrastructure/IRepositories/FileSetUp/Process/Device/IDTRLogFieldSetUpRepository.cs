using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device
{
    public interface IDTRLogFieldSetUpRepository
    {
        Task<DTRLogFieldsSetUpDTO> InsertAsync(DTRLogFieldsSetUpDTO data);
        Task<DTRLogFieldsSetUpDTO> UpdateAsync(DTRLogFieldsSetUpDTO data);
        Task<bool> DeleteAsync(int id);
        Task<DTRLogFieldsSetUpDTO?> GetByIdAsync(int id);
        Task<List<DTRLogFieldsSetUpDTO>> GetAllAsync();
    }
}
