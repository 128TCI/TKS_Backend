using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;

namespace WebbApp.Controllers.FileSetUp.Employment
{
    [Route("api/Fs/Employment/[controller]")]
    [ApiController]
    public class DesignationSetUpController : ControllerBase
    {
        private readonly IDesignationSetUpService _DesignationSetUpService;

        public DesignationSetUpController(IDesignationSetUpService designationService)
        {
            _DesignationSetUpService = designationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DesignationSetUpDTO>>> GetAll()
        {
            var employees = await _DesignationSetUpService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DesignationSetUpDTO>> GetById(int id)
        {
            var employee = await _DesignationSetUpService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<DesignationSetUpDTO>> Create([FromBody] DesignationSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _DesignationSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DesignationSetUpDTO>> Update(int id, [FromBody] DesignationSetUpDTO dto)
        {
            if (id != dto.DesID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _DesignationSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _DesignationSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
