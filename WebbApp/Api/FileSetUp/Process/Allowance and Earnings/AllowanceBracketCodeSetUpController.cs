using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;
using Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings;
using Services.Services.FileSetUp.Process.Allowance_and_Earnings;

namespace WebbApp.Api.FileSetUp.Process.Allowance_and_Earnings
{
    [Route("api/Fs/Process/AllowanceAndEarnings/[controller]")]
    [ApiController]
    public class AllowanceBracketCodeSetUpController : ControllerBase
    {
        private readonly IAllowanceBracketCodeSetUpService _AllowanceBracketCodeSetUpService;

        public AllowanceBracketCodeSetUpController(IAllowanceBracketCodeSetUpService allowanceBracketCodeSetUpService)
        {
            _AllowanceBracketCodeSetUpService = allowanceBracketCodeSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AllowanceBracketCodeSetUpDTO>>> GetAll()
        {
            var allowanceBracketCodeSetUpService = await _AllowanceBracketCodeSetUpService.GetAllAsync();
            return Ok(allowanceBracketCodeSetUpService);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AllowanceBracketCodeSetUpDTO>> GetById(int id)
        {
            var allowanceBracketCodeSetUpService = await _AllowanceBracketCodeSetUpService.GetByIdAsync(id);
            if (allowanceBracketCodeSetUpService == null)
                return NotFound();

            return Ok(allowanceBracketCodeSetUpService);
        }

        [HttpPost]
        public async Task<ActionResult<AllowanceBracketCodeSetUpDTO>> Create([FromBody] AllowanceBracketCodeSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _AllowanceBracketCodeSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AllowanceBracketCodeSetUpDTO>> Update(int id, [FromBody] AllowanceBracketCodeSetUpDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _AllowanceBracketCodeSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _AllowanceBracketCodeSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}

