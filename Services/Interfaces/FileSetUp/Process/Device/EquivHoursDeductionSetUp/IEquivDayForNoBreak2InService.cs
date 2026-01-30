using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device.EquivHoursDeductionSetUp
{
    public interface IEquivDayForNoBreak2InService
    {
        Task<EquivDayForNoBreat2InDTO> CreateAsync(EquivDayForNoBreat2InDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<EquivDayForNoBreat2InDTO> UpdateAsync(EquivDayForNoBreat2InDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<EquivDayForNoBreat2InDTO?> GetByIdAsync(int id);
        Task<List<EquivDayForNoBreat2InDTO>> GetAllAsync();
    }
}
