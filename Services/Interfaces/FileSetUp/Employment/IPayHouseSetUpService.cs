using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface IPayHouseSetUpService
    {
        Task<PayHouseSetUpDTO> CreateAsync(PayHouseSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<PayHouseSetUpDTO> UpdateAsync(PayHouseSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<PayHouseSetUpDTO?> GetByIdAsync(int id);
        Task<List<PayHouseSetUpDTO>> GetAllAsync();
    }
}
