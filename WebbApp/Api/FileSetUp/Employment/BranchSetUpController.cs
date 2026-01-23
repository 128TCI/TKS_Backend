using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;

namespace WebbApp.Controllers.FileSetUp.Employment
{
    [Route("api/Fs/Employment/[controller]")]
    [ApiController]
    public class BranchSetUpController : ControllerBase
    {
        private readonly IBranchSetUpService _BranchSetUpService;

        public BranchSetUpController(IBranchSetUpService branchService)
        {
            _BranchSetUpService = branchService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BranchSetUpDTO>>> GetAll()
        {
            var employees = await _BranchSetUpService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BranchSetUpDTO>> GetById(int id)
        {
            var employee = await _BranchSetUpService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<BranchSetUpDTO>> Create([FromBody] BranchSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _BranchSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BranchSetUpDTO>> Update(int id, [FromBody] BranchSetUpDTO dto)
        {
            if (id != dto.BraID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _BranchSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _BranchSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
