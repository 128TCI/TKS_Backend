using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Device;
using Infrastructure.IRepositories.FileSetUp.Process;
using Infrastructure.IRepositories.FileSetUp.Process.Device;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Process;
using Services.Interfaces.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Process
{
    public class HelpSetUpService : IHelpSetUpService
    {
        private readonly IHelpSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService;
        private readonly string _folderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploaded", "HelpSetUp");

        public HelpSetUpService(IHelpSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;

            // Ensure directory exists on initialization
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);            }
        }

        public async Task<HelpSetUpDTO> CreateAsync(HelpSetUpDTO dto, CancellationToken ct = default)
        {
            if (dto.File != null && dto.File.Length > 0)
            {
                dto.FileName = await SaveFileToDisk(dto);
            }

            var entity = MapToEntity(dto);
            var result = await _repository.InsertAsync(entity);

            return MapToDTO(result);
        }

        public async Task<HelpSetUpDTO> UpdateAsync(HelpSetUpDTO dto)
        {
            var existingRecord = await _repository.GetByIdAsync(dto.ID);
            if (existingRecord == null) return null;

            if (dto.File != null && dto.File.Length > 0)
            {
                // Delete old file first
                DeletePhysicalFile(existingRecord.FileName);

                // Save new file
                var newFileName = await SaveFileToDisk(dto);
                if (newFileName != null)
                {
                    dto.FileName = newFileName;
                }
            }
            else
            {
                // If no new file is uploaded, keep the old filename
                dto.FileName = existingRecord.FileName;
            }

            existingRecord.Code = dto.Code;
            existingRecord.Description = dto.Description;
            existingRecord.FileName = dto.FileName;

            var result = await _repository.UpdateAsync(existingRecord);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingRecord = await _repository.GetByIdAsync(id);
            if (existingRecord != null)
            {
                // Remove physical file when record is deleted
                DeletePhysicalFile(existingRecord.FileName);
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<HelpSetUpDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : MapToDTO(entity);
        }

        public async Task<List<HelpSetUpDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            if (entities == null || !entities.Any())
            {
                return new List<HelpSetUpDTO>();
            }

            return entities.Select(MapToDTO).ToList();
        }

        // --- Helper Methods ---

        private async Task<string> SaveFileToDisk(HelpSetUpDTO dto)
        {
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            // Best Practice: Append a timestamp or GUID to make the filename unique
            string fileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
            string fullPath = Path.Combine(_folderPath, fileName);

            // FileMode.Create will overwrite if the file exists, 
            // but since we added a GUID above, it will be unique anyway.
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            return fileName;
        }

        private void DeletePhysicalFile(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            string fullPath = Path.Combine(_folderPath, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        // --- Mapping Logic ---

        private HelpSetUpDTO MapToEntity(HelpSetUpDTO dto)
        {
            return new HelpSetUpDTO
            {
                ID = dto.ID,
                Code = dto.Code,
                Description = dto.Description,
                FileName = dto.FileName
            };
        }

        private HelpSetUpDTO MapToDTO(HelpSetUpDTO entity)
        {
            return new HelpSetUpDTO
            {
                ID = entity.ID,
                Code = entity.Code?.Trim(),
                Description = entity.Description,
                FileName = entity.FileName,
                // Add a helper property for the frontend if needed:
                // FileUrl = $"/uploaded/HelpSetUp/{entity.FileName}"
            };
        }
    }
}