using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;

namespace WebbApp.Api.FileSetUp.Employment
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationSetUpController : ControllerBase
    {
        private readonly ILocationSetUpService _LocationSetUpService;

        public LocationSetUpController(ILocationSetUpService locationSetUpService)
        {
            _LocationSetUpService = locationSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LocationSetUpDTO>>> GetAll()
        {
            var employeeStatus = await _LocationSetUpService.GetAllAsync();
            return Ok(employeeStatus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationSetUpDTO>> GetById(int id)
        {
            var employeeStatus = await _LocationSetUpService.GetByIdAsync(id);
            if (employeeStatus == null)
                return NotFound();

            return Ok(employeeStatus);
        }

        [HttpPost]
        public async Task<ActionResult<LocationSetUpDTO>> Create([FromBody] LocationSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _LocationSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LocationSetUpDTO>> Update(int id, [FromBody] LocationSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _LocationSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _LocationSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
