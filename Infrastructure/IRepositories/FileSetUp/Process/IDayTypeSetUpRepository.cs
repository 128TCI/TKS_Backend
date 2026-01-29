using DomainEntities.DTO.FileSetUp.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process
{
    public interface IDayTypeSetUpRepository
    {
        Task<DayTypeSetUpDTO> InsertAsync(DayTypeSetUpDTO calendarSetUp);
        Task<DayTypeSetUpDTO> UpdateAsync(DayTypeSetUpDTO calendarSetUp);
        Task<bool> DeleteAsync(int id);
        Task<DayTypeSetUpDTO?> GetByIdAsync(int id);
        Task<List<DayTypeSetUpDTO>> GetAllAsync();
    }
}
