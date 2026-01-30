using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device.EquivHoursDeductionSetUp
{
    public interface IEquivDayForNoLogoutService
    {
        Task<EquivDayForNoLogoutDTO> CreateAsync(EquivDayForNoLogoutDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<EquivDayForNoLogoutDTO> UpdateAsync(EquivDayForNoLogoutDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<EquivDayForNoLogoutDTO?> GetByIdAsync(int id);
        Task<List<EquivDayForNoLogoutDTO>> GetAllAsync();
    }
}
