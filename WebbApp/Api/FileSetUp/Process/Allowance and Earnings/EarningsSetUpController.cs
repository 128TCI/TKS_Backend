using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings;

namespace WebbApp.Api.FileSetUp.Process.Allowance_and_Earnings
{
    [Route("api/Fs/Process/AllowanceAndEarnings/[controller]")]
    [ApiController]
    public class EarningsSetUpController : ControllerBase
    {
        private readonly IEarningsSetUpService _EarningsSetUpService;

        public EarningsSetUpController(IEarningsSetUpService earningsSetUpService)
        {
            _EarningsSetUpService = earningsSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EarningSetUpDTO>>> GetAll()
        {
            var earningsSetUpService = await _EarningsSetUpService.GetAllAsync();
            return Ok(earningsSetUpService);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EarningSetUpDTO>> GetById(int id)
        {
            var earningsSetUpService = await _EarningsSetUpService.GetByIdAsync(id);
            if (earningsSetUpService == null)
                return NotFound();

            return Ok(earningsSetUpService);
        }

        [HttpPost]
        public async Task<ActionResult<EarningSetUpDTO>> Create([FromBody] EarningSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _EarningsSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EarningSetUpDTO>> Update(int id, [FromBody] EarningSetUpDTO dto)
        {
            if (id != dto.EarnID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _EarningsSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _EarningsSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
