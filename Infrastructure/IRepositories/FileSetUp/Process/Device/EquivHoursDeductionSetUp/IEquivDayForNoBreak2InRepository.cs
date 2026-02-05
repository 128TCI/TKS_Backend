using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device.EquivHoursDeductionSetUp
{
    public interface IEquivDayForNoBreak2InRepository
    {
        Task<EquivDayForNoBreat2InDTO> InsertAsync(EquivDayForNoBreat2InDTO data);
        Task<EquivDayForNoBreat2InDTO> UpdateAsync(EquivDayForNoBreat2InDTO data);
        Task<bool> DeleteAsync(int id);
        Task<EquivDayForNoBreat2InDTO?> GetByIdAsync(int id);
        Task<List<EquivDayForNoBreat2InDTO>> GetAllAsync();
    }
}
