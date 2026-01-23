using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface IDepartmentSetUpService
    {
        Task<DepartmentSetUpDTO> CreateAsync(DepartmentSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<DepartmentSetUpDTO> UpdateAsync(DepartmentSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<DepartmentSetUpDTO?> GetByIdAsync(int id);
        Task<List<DepartmentSetUpDTO>> GetAllAsync();
    }
}
