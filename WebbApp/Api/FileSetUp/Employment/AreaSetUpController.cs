using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.Employee;
using Services.Interfaces.FileSetUp.Employment;


namespace WebbApp.Controllers.FileSetUp.Employment
{
    [Route("api/Fs/Employment[controller]")]
    [ApiController]
    public class AreaSetUpController : ControllerBase
    {
        private readonly IAreaSetUpService _AreaSetUpService;

        public AreaSetUpController(IAreaSetUpService employeeService)
        {
            _AreaSetUpService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AreaSetUpDTO>>> GetAll()
        {
            var employees = await _AreaSetUpService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AreaSetUpDTO>> GetById(int id)
        {
            var employee = await _AreaSetUpService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<AreaSetUpDTO>> Create([FromBody] AreaSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _AreaSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AreaSetUpDTO>> Update(int id, [FromBody] AreaSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _AreaSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _AreaSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
