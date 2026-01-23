using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;

namespace WebbApp.Api.FileSetUp.Employment
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitSetUpController : ControllerBase
    {
        private readonly IUnitSetUpService _UnitSetUpService;

        public UnitSetUpController(IUnitSetUpService unitSetUpService)
        {
            _UnitSetUpService = unitSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UnitSetUpDTO>>> GetAll()
        {
            var employeeStatus = await _UnitSetUpService.GetAllAsync();
            return Ok(employeeStatus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitSetUpDTO>> GetById(int id)
        {
            var employeeStatus = await _UnitSetUpService.GetByIdAsync(id);
            if (employeeStatus == null)
                return NotFound();

            return Ok(employeeStatus);
        }

        [HttpPost]
        public async Task<ActionResult<UnitSetUpDTO>> Create([FromBody] UnitSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _UnitSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UnitSetUpDTO>> Update(int id, [FromBody] UnitSetUpDTO dto)
        {
            if (id != dto.UnitID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _UnitSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _UnitSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
