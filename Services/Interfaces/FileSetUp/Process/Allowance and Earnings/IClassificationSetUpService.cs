using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings
{
    public interface IClassificationSetUpService
    {
        Task<ClassificationSetUpDTO> CreateAsync(ClassificationSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<ClassificationSetUpDTO> UpdateAsync(ClassificationSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<ClassificationSetUpDTO?> GetByIdAsync(int id);
        Task<List<ClassificationSetUpDTO>> GetAllAsync();
    }
}
