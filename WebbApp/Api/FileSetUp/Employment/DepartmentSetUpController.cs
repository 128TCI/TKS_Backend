using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;

namespace WebbApp.Controllers.FileSetUp.Employment
{
    [Route("api/Fs/Employment/[controller]")]
    [ApiController]
    public class DepartmentSetUpController : ControllerBase
    {
        private readonly IDepartmentSetUpService _DepartmentSetUpService;

        public DepartmentSetUpController(IDepartmentSetUpService departmentService)
        {
            _DepartmentSetUpService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentSetUpDTO>>> GetAll()
        {
            var employees = await _DepartmentSetUpService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentSetUpDTO>> GetById(int id)
        {
            var employee = await _DepartmentSetUpService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentSetUpDTO>> Create([FromBody] DepartmentSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _DepartmentSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentSetUpDTO>> Update(int id, [FromBody] DepartmentSetUpDTO dto)
        {
            if (id != dto.DepID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _DepartmentSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _DepartmentSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
}
}
