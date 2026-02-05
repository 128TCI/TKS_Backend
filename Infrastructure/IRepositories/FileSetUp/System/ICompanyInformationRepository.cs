using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.FileSetUp.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.System
{
    public interface ICompanyInformationRepository
    {
        Task<CompanyInformationDTO> InsertAsync(CompanyInformationDTO companyInformation);

        Task<CompanyInformationDTO> UpdateAsync(CompanyInformationDTO companyInformation);

        Task<bool> DeleteAsync(int id);

        Task<CompanyInformationDTO?> GetByIdAsync(int id);

        Task<List<CompanyInformationDTO>> GetAllAsync();
    }
}
