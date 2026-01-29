using DomainEntities.DTO.FileSetUp.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process
{
    public interface ICalendarSetUpService
    {
        Task<CalendarSetUpDTO> CreateAsync(CalendarSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<CalendarSetUpDTO> UpdateAsync(CalendarSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<CalendarSetUpDTO?> GetByIdAsync(int id);
        Task<List<CalendarSetUpDTO>> GetAllAsync();
    }
}
