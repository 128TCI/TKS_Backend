using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface IEmployeeStatusSetUpService
    {
    Task<EmployeeStatusSetUpDTO> CreateAsync(EmployeeStatusSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
    Task<EmployeeStatusSetUpDTO> UpdateAsync(EmployeeStatusSetUpDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<EmployeeStatusSetUpDTO?> GetByIdAsync(int id);
    Task<List<EmployeeStatusSetUpDTO>> GetAllAsync();
    }
}
