using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface IDesignationSetUpRepository
    {
        Task<DesignationSetUp> InsertAsync(DesignationSetUp designationtSetUp);

        Task<DesignationSetUp> UpdateAsync(DesignationSetUp designationtSetUp);

        Task<bool> DeleteAsync(int id);

        Task<DesignationSetUp?> GetByIdAsync(int id);

        Task<List<DesignationSetUp>> GetAllAsync();
    }
}
