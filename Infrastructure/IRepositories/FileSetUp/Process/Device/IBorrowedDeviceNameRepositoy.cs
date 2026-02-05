using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device
{
    public interface IBorrowedDeviceNameRepositoy
    {
        Task<BorrowedDeviceNameDTO> InsertAsync(BorrowedDeviceNameDTO data);
        Task<BorrowedDeviceNameDTO> UpdateAsync(BorrowedDeviceNameDTO data);
        Task<bool> DeleteAsync(int id);
        Task<BorrowedDeviceNameDTO?> GetByIdAsync(int id);
        Task<List<BorrowedDeviceNameDTO>> GetAllAsync();
    }
}
