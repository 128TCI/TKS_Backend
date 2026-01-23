using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;

namespace WebbApp.Controllers.FileSetUp.Employment
{
    [Route("api/Fs/Employment/[controller]")]
    [ApiController]
    public class DivisionSetUpController : ControllerBase
    {
        private readonly IDivisionSetUpService _DivisionSetUpService;

        public DivisionSetUpController(IDivisionSetUpService divisionService)
        {
            _DivisionSetUpService = divisionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DivisionSetUpDTO>>> GetAll()
        {
            var employees = await _DivisionSetUpService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DivisionSetUpDTO>> GetById(int id)
        {
            var employee = await _DivisionSetUpService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<DivisionSetUpDTO>> Create([FromBody] DivisionSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _DivisionSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DivisionSetUpDTO>> Update(int id, [FromBody] DivisionSetUpDTO dto)
        {
            if (id != dto.DivID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _DivisionSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _DivisionSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
