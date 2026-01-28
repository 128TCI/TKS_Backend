using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;

namespace WebbApp.Api.FileSetUp.Employment
{
    [Route("api/Fs/Employment/[controller]")]
    [ApiController]
    public class JobLevelSetUpController : ControllerBase
    {
        private readonly IJobLevelSetUpService _JobLevelSetUpService;

        public JobLevelSetUpController(IJobLevelSetUpService jobLevelSetUpService)
        {
            _JobLevelSetUpService = jobLevelSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<JobLevelSetUpDTO>>> GetAll()
        {
            var employeeStatus = await _JobLevelSetUpService.GetAllAsync();
            return Ok(employeeStatus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobLevelSetUpDTO>> GetById(int id)
        {
            var employeeStatus = await _JobLevelSetUpService.GetByIdAsync(id);
            if (employeeStatus == null)
                return NotFound();

            return Ok(employeeStatus);
        }

        [HttpPost]
        public async Task<ActionResult<JobLevelSetUpDTO>> Create([FromBody] JobLevelSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _JobLevelSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JobLevelSetUpDTO>> Update(int id, [FromBody] JobLevelSetUpDTO dto)
        {
            if (id != dto.JobLevelID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _JobLevelSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _JobLevelSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
