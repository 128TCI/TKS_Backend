using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface IJobLevelSetUpRepository
    {
        Task<JobLevelSetUp> InsertAsync(JobLevelSetUp groupSetUp);

        Task<JobLevelSetUp> UpdateAsync(JobLevelSetUp groupSetUp);

        Task<bool> DeleteAsync(int id);

        Task<JobLevelSetUp?> GetByIdAsync(int id);

        Task<List<JobLevelSetUp>> GetAllAsync();
    }
}
