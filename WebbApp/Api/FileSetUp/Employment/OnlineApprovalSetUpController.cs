using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;

namespace WebbApp.Api.FileSetUp.Employment
{
    [Route("api/Fs/Employment/[controller]")]
    [ApiController]
    public class OnlineApprovalSetUpController : ControllerBase
    {
        private readonly IOnlineApprovalSetUpService _OnlineApprovalSetUpService;

        public OnlineApprovalSetUpController(IOnlineApprovalSetUpService onlineApprovalSetUpService)
        {
            _OnlineApprovalSetUpService = onlineApprovalSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OnlineApprovalSetUpDTO>>> GetAll()
        {
            var employeeStatus = await _OnlineApprovalSetUpService.GetAllAsync();
            return Ok(employeeStatus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OnlineApprovalSetUpDTO>> GetById(int id)
        {
            var employeeStatus = await _OnlineApprovalSetUpService.GetByIdAsync(id);
            if (employeeStatus == null)
                return NotFound();

            return Ok(employeeStatus);
        }

        [HttpPost]
        public async Task<ActionResult<OnlineApprovalSetUpDTO>> Create([FromBody] OnlineApprovalSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _OnlineApprovalSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OnlineApprovalSetUpDTO>> Update(int id, [FromBody] OnlineApprovalSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _OnlineApprovalSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _OnlineApprovalSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
