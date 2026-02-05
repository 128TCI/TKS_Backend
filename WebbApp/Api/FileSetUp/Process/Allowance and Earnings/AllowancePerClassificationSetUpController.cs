using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings;
using Services.Services.FileSetUp.Process.Allowance_and_Earnings;

namespace WebbApp.Api.FileSetUp.Process.Allowance_and_Earnings
{
    [Route("api/Fs/Process/AllowanceAndEarnings/[controller]")]
    [ApiController]
    public class AllowancePerClassificationSetUpController : ControllerBase
    {
        private readonly IAllowancePerClassificationSetUpService _AllowancePerClassificationSetUpService;

        public AllowancePerClassificationSetUpController(IAllowancePerClassificationSetUpService allowancePerClassificationSetUpService)
        {
            _AllowancePerClassificationSetUpService = allowancePerClassificationSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AllowancePerClassificationSetUpDTO>>> GetAll()
        {
            var allowancePerClassificationSetUpService = await _AllowancePerClassificationSetUpService.GetAllAsync();
            return Ok(allowancePerClassificationSetUpService);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AllowancePerClassificationSetUpDTO>> GetById(int id)
        {
            var allowancePerClassificationSetUpService = await _AllowancePerClassificationSetUpService.GetByIdAsync(id);
            if (allowancePerClassificationSetUpService == null)
                return NotFound();

            return Ok(allowancePerClassificationSetUpService);
        }

        [HttpPost]
        public async Task<ActionResult<AllowancePerClassificationSetUpDTO>> Create([FromBody] AllowancePerClassificationSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _AllowancePerClassificationSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AllowancePerClassificationSetUpDTO>> Update(int id, [FromBody] AllowancePerClassificationSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _AllowancePerClassificationSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _AllowancePerClassificationSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
