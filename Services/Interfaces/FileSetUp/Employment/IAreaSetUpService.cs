
using Services.DTOs;
using Services.DTOs.FileSetup.Employment;


namespace Services.Interfaces.FileSetUp.Employment;

    public interface IAreaSetUpService
    {
    Task<AreaSetUpDTO> CreateAsync(AreaSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
    Task<AreaSetUpDTO> UpdateAsync(AreaSetUpDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<AreaSetUpDTO?> GetByIdAsync(int id);
    Task<List<AreaSetUpDTO>> GetAllAsync();
}

