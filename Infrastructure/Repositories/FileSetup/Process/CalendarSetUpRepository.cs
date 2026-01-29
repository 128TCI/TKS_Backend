using DomainEntities.DTO.FileSetUp.Process;
using Infrastructure.IRepositories.FileSetUp.Process;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.Process
{
    public class CalendarSetUpRepository : ICalendarSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public CalendarSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<CalendarSetUpDTO> InsertAsync(CalendarSetUpDTO calendarSetUp)
        {
            _context.tk_Calendar.Add(calendarSetUp);
            await _context.SaveChangesAsync();
            return calendarSetUp;
        }

        public async Task<CalendarSetUpDTO> UpdateAsync(CalendarSetUpDTO calendarSetUp)
        {
            _context.tk_Calendar.Update(calendarSetUp);
            await _context.SaveChangesAsync();
            return calendarSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var timeKeepGroupSetUp = await _context.tk_Calendar.FindAsync(id);
            if (timeKeepGroupSetUp == null) return false;

            _context.tk_Calendar.Remove(timeKeepGroupSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CalendarSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_Calendar.FindAsync(id);
        }

        public async Task<List<CalendarSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_Calendar.ToListAsync();
        }
    }
}
