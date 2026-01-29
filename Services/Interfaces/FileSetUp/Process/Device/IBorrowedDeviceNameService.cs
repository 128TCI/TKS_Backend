using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device
{
    public interface IBorrowedDeviceNameService
    {
        Task<BorrowedDeviceNameDTO> CreateAsync(BorrowedDeviceNameDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<BorrowedDeviceNameDTO> UpdateAsync(BorrowedDeviceNameDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<BorrowedDeviceNameDTO?> GetByIdAsync(int id);
        Task<List<BorrowedDeviceNameDTO>> GetAllAsync();
    }
}
