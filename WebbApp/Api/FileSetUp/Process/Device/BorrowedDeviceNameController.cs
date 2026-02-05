using DomainEntities.DTO.FileSetUp.Process.Device;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Device;

namespace WebbApp.Api.FileSetUp.Process.Device
{
    [Route("api/Fs/Process/Device/[controller]")]
    [ApiController]
    public class BorrowedDeviceNameController : ControllerBase
    {
        private readonly IBorrowedDeviceNameService _BorrowedDeviceNameService;

        public BorrowedDeviceNameController(IBorrowedDeviceNameService service)
        {
            _BorrowedDeviceNameService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<BorrowedDeviceNameDTO>>> GetAll()
        {
            var data = await _BorrowedDeviceNameService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowedDeviceNameDTO>> GetById(int id)
        {
            var data = await _BorrowedDeviceNameService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<BorrowedDeviceNameDTO>> Create([FromBody] BorrowedDeviceNameDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _BorrowedDeviceNameService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BorrowedDeviceNameDTO>> Update(int id, [FromBody] BorrowedDeviceNameDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _BorrowedDeviceNameService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _BorrowedDeviceNameService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
