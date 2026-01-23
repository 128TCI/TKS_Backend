using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;

namespace WebbApp.Api.FileSetUp.Employment
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionSetUpController : ControllerBase
    {
        private readonly ISectionSetUpService _SectionSetUpService;

        public SectionSetUpController(ISectionSetUpService payHouseSetUpService)
        {
            _SectionSetUpService = payHouseSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SectionSetUpDTO>>> GetAll()
        {
            var employeeStatus = await _SectionSetUpService.GetAllAsync();
            return Ok(employeeStatus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SectionSetUpDTO>> GetById(int id)
        {
            var employeeStatus = await _SectionSetUpService.GetByIdAsync(id);
            if (employeeStatus == null)
                return NotFound();

            return Ok(employeeStatus);
        }

        [HttpPost]
        public async Task<ActionResult<SectionSetUpDTO>> Create([FromBody] SectionSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _SectionSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SectionSetUpDTO>> Update(int id, [FromBody] SectionSetUpDTO dto)
        {
            if (id != dto.SecID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _SectionSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _SectionSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
