using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface IJobLevelSetUpService
    {
        Task<JobLevelSetUpDTO> CreateAsync(JobLevelSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<JobLevelSetUpDTO> UpdateAsync(JobLevelSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<JobLevelSetUpDTO?> GetByIdAsync(int id);
        Task<List<JobLevelSetUpDTO>> GetAllAsync();
    }
}
