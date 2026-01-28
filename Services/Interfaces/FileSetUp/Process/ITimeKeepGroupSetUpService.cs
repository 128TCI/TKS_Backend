using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process
{
    public interface ITimeKeepGroupSetUpService
    {
        Task<TimeKeepGroupSetUpDTO> CreateAsync(TimeKeepGroupSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<TimeKeepGroupSetUpDTO> UpdateAsync(TimeKeepGroupSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<TimeKeepGroupSetUpDTO?> GetByIdAsync(int id);
        Task<List<TimeKeepGroupSetUpDTO>> GetAllAsync();
    }
}
