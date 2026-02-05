using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device
{
    public interface IMySQLDbConfigSetUpRepository
    {
        Task<MySQLDbConfigSetUpDTO> InsertAsync(MySQLDbConfigSetUpDTO data);
        Task<MySQLDbConfigSetUpDTO> UpdateAsync(MySQLDbConfigSetUpDTO data);
        Task<bool> DeleteAsync(int id);
        Task<MySQLDbConfigSetUpDTO?> GetByIdAsync(int id);
        Task<List<MySQLDbConfigSetUpDTO>> GetAllAsync();
    }
}
