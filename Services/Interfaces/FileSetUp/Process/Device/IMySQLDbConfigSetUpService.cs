using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device
{
    public interface IMySQLDbConfigSetUpService
    {
        Task<MySQLDbConfigSetUpDTO> CreateAsync(MySQLDbConfigSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<MySQLDbConfigSetUpDTO> UpdateAsync(MySQLDbConfigSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<MySQLDbConfigSetUpDTO?> GetByIdAsync(int id);
        Task<List<MySQLDbConfigSetUpDTO>> GetAllAsync();
    }
}
