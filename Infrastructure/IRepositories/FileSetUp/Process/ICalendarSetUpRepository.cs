using DomainEntities.DTO.FileSetUp.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process
{
    public interface ICalendarSetUpRepository
    {
        Task<CalendarSetUpDTO> InsertAsync(CalendarSetUpDTO calendarSetUp);
        Task<CalendarSetUpDTO> UpdateAsync(CalendarSetUpDTO calendarSetUp);
        Task<bool> DeleteAsync(int id);
        Task<CalendarSetUpDTO?> GetByIdAsync(int id);
        Task<List<CalendarSetUpDTO>> GetAllAsync();
    }
}
