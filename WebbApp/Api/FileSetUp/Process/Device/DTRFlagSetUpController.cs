using DomainEntities.DTO.FileSetUp.Process.Device;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Device;

namespace WebbApp.Api.FileSetUp.Process.Device
{
    [Route("api/Fs/Process/Device/[controller]")]
    [ApiController]
    public class DTRFlagSetUpController : ControllerBase
    {
        private readonly IDTRFlagSetUpService _DTRFlagSetUpService;

        public DTRFlagSetUpController(IDTRFlagSetUpService service)
        {
            _DTRFlagSetUpService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<DTRFlagSetUpDTO>>> GetAll()
        {
            var data = await _DTRFlagSetUpService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTRFlagSetUpDTO>> GetById(int id)
        {
            var data = await _DTRFlagSetUpService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<DTRFlagSetUpDTO>> Create([FromBody] DTRFlagSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _DTRFlagSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DTRFlagSetUpDTO>> Update(int id, [FromBody] DTRFlagSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _DTRFlagSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _DTRFlagSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
