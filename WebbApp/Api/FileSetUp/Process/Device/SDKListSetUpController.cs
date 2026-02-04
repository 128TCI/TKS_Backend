using DomainEntities.DTO.FileSetUp.Process.Device;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Device;

namespace WebbApp.Api.FileSetUp.Process.Device
{
    [Route("api/Fs/Process/Device/[controller]")]
    [ApiController]
    public class SDKListSetUpController : ControllerBase
    {
        private readonly ISDKListSetUpService _SDKListSetUpService;

        public SDKListSetUpController(ISDKListSetUpService service)
        {
            _SDKListSetUpService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<SDKListSetUpDTO>>> GetAll()
        {
            var data = await _SDKListSetUpService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SDKListSetUpDTO>> GetById(int id)
        {
            var data = await _SDKListSetUpService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<SDKListSetUpDTO>> Create([FromBody] SDKListSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _SDKListSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SDKListSetUpDTO>> Update(int id, [FromBody] SDKListSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _SDKListSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _SDKListSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
