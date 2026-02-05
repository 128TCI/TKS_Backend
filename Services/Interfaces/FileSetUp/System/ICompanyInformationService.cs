using DomainEntities.DTO.FileSetUp.System;
using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.System
{
    public interface ICompanyInformationService
    {
        Task<CompanyInformationDTO> CreateAsync(CompanyInformationDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<CompanyInformationDTO> UpdateAsync(CompanyInformationDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<CompanyInformationDTO?> GetByIdAsync(int id);
        Task<List<CompanyInformationDTO>> GetAllAsync();
    }
}
