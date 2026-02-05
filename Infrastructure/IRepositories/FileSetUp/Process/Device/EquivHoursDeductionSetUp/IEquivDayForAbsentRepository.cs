using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device.EquivHoursDeductionSetUp
{
    public interface IEquivDayForAbsentRepository
    {
        Task<EquivDayForAbsentDTO> InsertAsync(EquivDayForAbsentDTO data);
        Task<EquivDayForAbsentDTO> UpdateAsync(EquivDayForAbsentDTO data);
        Task<bool> DeleteAsync(int id);
        Task<EquivDayForAbsentDTO?> GetByIdAsync(int id);
        Task<List<EquivDayForAbsentDTO>> GetAllAsync();
    }

}
