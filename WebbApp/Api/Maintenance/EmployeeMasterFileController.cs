using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.DTOs.Maintenance;
using Services.Interfaces.FileSetUp.Employment;
using Services.Interfaces.Maintenence;

namespace WebbApp.Api.Maintenance
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeMasterFileController : ControllerBase
    {
        private readonly IEmployeeMasterFileService _EmployeeMasterFileService;

        public EmployeeMasterFileController(IEmployeeMasterFileService unitSetUpService)
        {
            _EmployeeMasterFileService = unitSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeMasterFileDTO>>> GetAll()
        {
            var employeeStatus = await _EmployeeMasterFileService.GetAllAsync();
            return Ok(employeeStatus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeMasterFileDTO>> GetById(int id)
        {
            var employeeStatus = await _EmployeeMasterFileService.GetByIdAsync(id);
            if (employeeStatus == null)
                return NotFound();

            return Ok(employeeStatus);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeMasterFileDTO>> Create([FromBody] EmployeeMasterFileDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _EmployeeMasterFileService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeMasterFileDTO>> Update(int id, [FromBody] EmployeeMasterFileDTO dto)
        {
            if (id != dto.EmpID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _EmployeeMasterFileService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _EmployeeMasterFileService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
