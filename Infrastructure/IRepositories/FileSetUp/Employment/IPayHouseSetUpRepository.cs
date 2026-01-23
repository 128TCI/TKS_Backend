using DomainEntities.DTO.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Employment
{
    public interface IPayHouseSetUpRepository
    {
        Task<PayHouseSetUp> InsertAsync(PayHouseSetUp payHouseSetUp);

        Task<PayHouseSetUp> UpdateAsync(PayHouseSetUp payHouseSetUp);

        Task<bool> DeleteAsync(int id);

        Task<PayHouseSetUp?> GetByIdAsync(int id);

        Task<List<PayHouseSetUp>> GetAllAsync();
    }
}
