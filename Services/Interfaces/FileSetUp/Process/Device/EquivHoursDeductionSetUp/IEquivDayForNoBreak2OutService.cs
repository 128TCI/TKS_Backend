using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device.EquivHoursDeductionSetUp
{
    public interface IEquivDayForNoBreak2OutService
    {
        Task<EquivDayForNoBreat2OutDTO> CreateAsync(EquivDayForNoBreat2OutDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<EquivDayForNoBreat2OutDTO> UpdateAsync(EquivDayForNoBreat2OutDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<EquivDayForNoBreat2OutDTO?> GetByIdAsync(int id);
        Task<List<EquivDayForNoBreat2OutDTO>> GetAllAsync();
    }
}
