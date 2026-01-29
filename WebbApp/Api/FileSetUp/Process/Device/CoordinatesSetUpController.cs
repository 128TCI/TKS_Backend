using DomainEntities.DTO.FileSetUp.Process.Device;
using Infrastructure.IRepositories.FileSetUp.Process.Device;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Device;

namespace WebbApp.Api.FileSetUp.Process.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinatesSetUpController : ControllerBase
    {
        private readonly ICoordinatesSetUpService _CoordinatesSetUpService;

        public CoordinatesSetUpController(ICoordinatesSetUpService service)
        {
            _CoordinatesSetUpService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CoordinatesSetUpDTO>>> GetAll()
        {
            var data = await _CoordinatesSetUpService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CoordinatesSetUpDTO>> GetById(int id)
        {
            var data = await _CoordinatesSetUpService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<CoordinatesSetUpDTO>> Create([FromBody] CoordinatesSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _CoordinatesSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CoordinatesSetUpDTO>> Update(int id, [FromBody] CoordinatesSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _CoordinatesSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _CoordinatesSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
    }
