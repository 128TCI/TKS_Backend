using DomainEntities.DTO.FileSetUp.Process.Device;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Device;

namespace WebbApp.Api.FileSetUp.Process.Device
{
    [Route("api/Fs/Process/Device/[controller]")]
    [ApiController]
    public class DeviceTypeSetUpController : ControllerBase
    {
        private readonly IDeviceTypeSetUpService _DeviceTypeSetUpService;

        public DeviceTypeSetUpController(IDeviceTypeSetUpService service)
        {
            _DeviceTypeSetUpService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeviceTypeSetUpDTO>>> GetAll()
        {
            var data = await _DeviceTypeSetUpService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceTypeSetUpDTO>> GetById(int id)
        {
            var data = await _DeviceTypeSetUpService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<DeviceTypeSetUpDTO>> Create([FromBody] DeviceTypeSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _DeviceTypeSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeviceTypeSetUpDTO>> Update(string id, [FromBody] DeviceTypeSetUpDTO dto)
        {
            if (id != dto.DeviceName)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _DeviceTypeSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _DeviceTypeSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        //DeviceType2
        [HttpGet("DeviceType2")]
        public async Task<ActionResult<List<DeviceType2SetUpDTO>>> GetAll2()
        {
            var data = await _DeviceTypeSetUpService.GetAll2Async();
            return Ok(data);
        }

        [HttpGet("DeviceType2{id}")]
        public async Task<ActionResult<DeviceType2SetUpDTO>> GetById2(int id)
        {
            var data = await _DeviceTypeSetUpService.GetById2Async(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost("DeviceType2")]
        public async Task<ActionResult<DeviceType2SetUpDTO>> Create2([FromBody] DeviceType2SetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _DeviceTypeSetUpService.Create2Async(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("DeviceType2{id}")]
        public async Task<ActionResult<DeviceType2SetUpDTO>> Update2(string id, [FromBody] DeviceType2SetUpDTO dto)
        {
            if (id != dto.DeviceName)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _DeviceTypeSetUpService.Update2Async(dto);
            return Ok(result);
        }

        [HttpDelete("DeviceType2/{id}")]
        public async Task<ActionResult> Delete2(int id)
        {
            var success = await _DeviceTypeSetUpService.Delete2Async(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
