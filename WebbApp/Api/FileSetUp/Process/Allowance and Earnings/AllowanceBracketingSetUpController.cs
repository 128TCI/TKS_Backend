using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings;

namespace WebbApp.Api.FileSetUp.Process.Allowance_and_Earnings
{
    [Route("api/Fs/Process/AllowanceAndEarnings/[controller]")]
    [ApiController]
    public class AllowanceBracketingSetUpController : ControllerBase
    {
        private readonly IAllowanceBracketingSetUpService _AllowanceBracketingSetUpService;

        public AllowanceBracketingSetUpController(IAllowanceBracketingSetUpService allowanceBracketingSetUpService)
        {
            _AllowanceBracketingSetUpService = allowanceBracketingSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AllowanceBracketingSetUpDTO>>> GetAll()
        {
            var allowanceBracketingSetUpService = await _AllowanceBracketingSetUpService.GetAllAsync();
            return Ok(allowanceBracketingSetUpService);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AllowanceBracketingSetUpDTO>> GetById(int id)
        {
            var allowanceBracketingSetUpService = await _AllowanceBracketingSetUpService.GetByIdAsync(id);
            if (allowanceBracketingSetUpService == null)
                return NotFound();

            return Ok(allowanceBracketingSetUpService);
        }

        [HttpPost]
        public async Task<ActionResult<AllowanceBracketingSetUpDTO>> Create([FromBody] AllowanceBracketingSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _AllowanceBracketingSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AllowanceBracketingSetUpDTO>> Update(int id, [FromBody] AllowanceBracketingSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _AllowanceBracketingSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _AllowanceBracketingSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
