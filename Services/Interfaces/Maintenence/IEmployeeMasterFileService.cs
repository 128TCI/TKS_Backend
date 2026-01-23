using Services.DTOs.FileSetup.Employment;
using Services.DTOs.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Maintenence
{
    public interface IEmployeeMasterFileService
    {
        Task<EmployeeMasterFileDTO> CreateAsync(EmployeeMasterFileDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<EmployeeMasterFileDTO> UpdateAsync(EmployeeMasterFileDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<EmployeeMasterFileDTO?> GetByIdAsync(int id);
        Task<List<EmployeeMasterFileDTO>> GetAllAsync();
    }
}
