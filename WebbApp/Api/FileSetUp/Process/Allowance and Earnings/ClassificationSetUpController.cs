using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings;

namespace WebbApp.Api.FileSetUp.Process.Allowance_and_Earnings
{
    [Route("api/Fs/Process/AllowanceAndEarnings/[controller]")]
    [ApiController]
    public class ClassificationSetUpController : ControllerBase
    {
        private readonly IClassificationSetUpService _ClassificationSetUpService;

        public ClassificationSetUpController(IClassificationSetUpService classificationSetUpService)
        {
            _ClassificationSetUpService = classificationSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClassificationSetUpDTO>>> GetAll()
        {
            var allowanceBracketCodeSetUpService = await _ClassificationSetUpService.GetAllAsync();
            return Ok(allowanceBracketCodeSetUpService);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassificationSetUpDTO>> GetById(int id)
        {
            var classificationSetUpService = await _ClassificationSetUpService.GetByIdAsync(id);
            if (classificationSetUpService == null)
                return NotFound();

            return Ok(classificationSetUpService);
        }

        [HttpPost]
        public async Task<ActionResult<ClassificationSetUpDTO>> Create([FromBody] ClassificationSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _ClassificationSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClassificationSetUpDTO>> Update(int id, [FromBody] ClassificationSetUpDTO dto)
        {
            if (id != dto.ClassId)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _ClassificationSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _ClassificationSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
