using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device.EquivHoursDeductionSetUp
{
    public interface IEquivDayForNoLogOutRepository
    {
        Task<EquivDayForNoLogoutDTO> InsertAsync(EquivDayForNoLogoutDTO data);
        Task<EquivDayForNoLogoutDTO> UpdateAsync(EquivDayForNoLogoutDTO data);
        Task<bool> DeleteAsync(int id);
        Task<EquivDayForNoLogoutDTO?> GetByIdAsync(int id);
        Task<List<EquivDayForNoLogoutDTO>> GetAllAsync();
    }
}
