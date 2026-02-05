using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings
{
    public interface IClassificationSetUpRepository
    {
        Task<ClassificationSetUpDTO> InsertAsync(ClassificationSetUpDTO sectionSetUp);
        Task<ClassificationSetUpDTO> UpdateAsync(ClassificationSetUpDTO sectionSetUp);
        Task<bool> DeleteAsync(int id);
        Task<ClassificationSetUpDTO?> GetByIdAsync(int id);
        Task<List<ClassificationSetUpDTO>> GetAllAsync();
    }
}
