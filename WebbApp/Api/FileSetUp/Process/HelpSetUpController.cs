using DomainEntities.DTO.FileSetUp.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process;

namespace WebbApp.Api.FileSetUp.Process
{
    [Route("api/Fs/Process/[controller]")]
    [ApiController]
    public class HelpSetUpController : ControllerBase
    {
        private readonly IHelpSetUpService _HelpSetUpService;

        public HelpSetUpController(IHelpSetUpService service)
        {
            _HelpSetUpService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<HelpSetUpDTO>>> GetAll()
        {
            var data = await _HelpSetUpService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HelpSetUpDTO>> GetById(int id)
        {
            var data = await _HelpSetUpService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<HelpSetUpDTO>> Create([FromForm] HelpSetUpDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Data is null");
            }

            // Optional: Basic validation to ensure a file was actually sent
            if (dto.File == null || string.IsNullOrEmpty(dto.File.FileName))
            {
                return BadRequest("A valid file is required.");
            }

            var result = await _HelpSetUpService.CreateAsync(dto);

            // If the service returned null, it means the filename validation failed
            if (result == null)
            {
                return Conflict(new
                {
                    message = $"Upload failed. The filename '{dto.File.FileName}' is already in use. Please rename the file or delete the existing one."
                });
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        // CHANGED: [FromBody] to [FromForm] to allow file replacement during update
        public async Task<ActionResult<HelpSetUpDTO>> Update(int id, [FromForm] HelpSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _HelpSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _HelpSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}