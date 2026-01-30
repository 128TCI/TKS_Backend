using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device.EquivHoursDeductionSetUp
{
    public interface IEquivDayForNoLoginRepository
    {
        Task<EquivDayForNoLoginDTO> InsertAsync(EquivDayForNoLoginDTO data);
        Task<EquivDayForNoLoginDTO> UpdateAsync(EquivDayForNoLoginDTO data);
        Task<bool> DeleteAsync(int id);
        Task<EquivDayForNoLoginDTO?> GetByIdAsync(int id);
        Task<List<EquivDayForNoLoginDTO>> GetAllAsync();
    }
}
