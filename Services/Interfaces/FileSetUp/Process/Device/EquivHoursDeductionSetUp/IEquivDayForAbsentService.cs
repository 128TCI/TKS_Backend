using DomainEntities.DTO.FileSetUp.Process.Device;
using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device.EquivHoursDeductionSetUp
{
    public interface IEquivDayForAbsentService
    {
        Task<EquivDayForAbsentDTO> CreateAsync(EquivDayForAbsentDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<EquivDayForAbsentDTO> UpdateAsync(EquivDayForAbsentDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<EquivDayForAbsentDTO?> GetByIdAsync(int id);
        Task<List<EquivDayForAbsentDTO>> GetAllAsync();
    }
}
