using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device.EquivHoursDeductionSetUp
{
    public interface IEquivDayForNoLoginService
    {
        Task<EquivDayForNoLoginDTO> CreateAsync(EquivDayForNoLoginDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<EquivDayForNoLoginDTO> UpdateAsync(EquivDayForNoLoginDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<EquivDayForNoLoginDTO?> GetByIdAsync(int id);
        Task<List<EquivDayForNoLoginDTO>> GetAllAsync();
    }
}
