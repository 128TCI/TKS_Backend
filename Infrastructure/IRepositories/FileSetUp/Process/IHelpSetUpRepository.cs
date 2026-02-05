using DomainEntities.DTO.FileSetUp.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process
{
    public interface IHelpSetUpRepository
    {
        Task<HelpSetUpDTO> InsertAsync(HelpSetUpDTO data);
        Task<HelpSetUpDTO> UpdateAsync(HelpSetUpDTO data);
        Task<bool> DeleteAsync(int id);
        Task<HelpSetUpDTO?> GetByIdAsync(int id);
        Task<List<HelpSetUpDTO>> GetAllAsync();
    }
}
