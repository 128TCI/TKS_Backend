using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device.EquivHoursDeductionSetUp
{
    public interface IEquivDayForNoBreak2OutRepository
    {
        Task<EquivDayForNoBreat2OutDTO> InsertAsync(EquivDayForNoBreat2OutDTO data);
        Task<EquivDayForNoBreat2OutDTO> UpdateAsync(EquivDayForNoBreat2OutDTO data);
        Task<bool> DeleteAsync(int id);
        Task<EquivDayForNoBreat2OutDTO?> GetByIdAsync(int id);
        Task<List<EquivDayForNoBreat2OutDTO>> GetAllAsync();
    }
}
