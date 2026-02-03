using DomainEntities.DTO.FileSetUp.Process.Device;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Device;

namespace WebbApp.Api.FileSetUp.Process.Device
{
    [Route("api/Fs/Process/Device/[controller]")]
    [ApiController]
    public class MySQLDbConfigSetUpController : ControllerBase
    {
        private readonly IMySQLDbConfigSetUpService _MySQLDbConfigSetUpService;

        public MySQLDbConfigSetUpController(IMySQLDbConfigSetUpService service)
        {
            _MySQLDbConfigSetUpService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<MySQLDbConfigSetUpDTO>>> GetAll()
        {
            var data = await _MySQLDbConfigSetUpService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MySQLDbConfigSetUpDTO>> GetById(int id)
        {
            var data = await _MySQLDbConfigSetUpService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<MySQLDbConfigSetUpDTO>> Create([FromBody] MySQLDbConfigSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _MySQLDbConfigSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MySQLDbConfigSetUpDTO>> Update(int id, [FromBody] MySQLDbConfigSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _MySQLDbConfigSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _MySQLDbConfigSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
