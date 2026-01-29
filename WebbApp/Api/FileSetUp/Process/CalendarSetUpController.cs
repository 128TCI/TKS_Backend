using DomainEntities.DTO.FileSetUp.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process;

namespace WebbApp.Api.FileSetUp.Process
{
    [Route("api/Fs/Process/[controller]")]
    [ApiController]
    public class CalendarSetUpController : ControllerBase
    {
        private readonly ICalendarSetUpService _CalendarSetUpService;

        public CalendarSetUpController(ICalendarSetUpService calendarSetUpService)
        {
            _CalendarSetUpService = calendarSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CalendarSetUpDTO>>> GetAll()
        {
            var calendarSetUpService = await _CalendarSetUpService.GetAllAsync();
            return Ok(calendarSetUpService);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarSetUpDTO>> GetById(int id)
        {
            var calendarSetUpService = await _CalendarSetUpService.GetByIdAsync(id);
            if (calendarSetUpService == null)
                return NotFound();

            return Ok(calendarSetUpService);
        }

        [HttpPost]
        public async Task<ActionResult<CalendarSetUpDTO>> Create([FromBody] CalendarSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _CalendarSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CalendarSetUpDTO>> Update(int id, [FromBody] CalendarSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _CalendarSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _CalendarSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
