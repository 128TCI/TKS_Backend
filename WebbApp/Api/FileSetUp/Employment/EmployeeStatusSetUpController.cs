using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;

namespace WebbApp.Api.FileSetUp.Employment
{
    [Route("api/Fs/Employment/[controller]")]
    [ApiController]
    public class EmployeeStatusSetUpController : ControllerBase
    {
        private readonly IEmployeeStatusSetUpService _EmployeeStatusService;

        public EmployeeStatusSetUpController(IEmployeeStatusSetUpService employeeStatusService)
        {
            _EmployeeStatusService = employeeStatusService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeStatusSetUpDTO>>> GetAll()
        {
            var employeeStatus = await _EmployeeStatusService.GetAllAsync();
            return Ok(employeeStatus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeStatusSetUpDTO>> GetById(int id)
        {
            var employeeStatus = await _EmployeeStatusService.GetByIdAsync(id);
            if (employeeStatus == null)
                return NotFound();

            return Ok(employeeStatus);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeStatusSetUpDTO>> Create([FromBody] EmployeeStatusSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _EmployeeStatusService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeStatusSetUpDTO>> Update(int id, [FromBody] EmployeeStatusSetUpDTO dto)
        {
            if (id != dto.EmpStatID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _EmployeeStatusService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _EmployeeStatusService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
