using DomainEntities.DTO.FileSetUp.Process.Device;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Device;

namespace WebbApp.Api.FileSetUp.Process.Device
{
    [Route("api/Fs/Process/Device/[controller]")]
    [ApiController]
    public class DTRLogFIeldsSetUpController : ControllerBase
    {
        private readonly IDTRLogFieldsSetUpService _DTRLogFieldsSetUpService;

        public DTRLogFIeldsSetUpController(IDTRLogFieldsSetUpService service)
        {
            _DTRLogFieldsSetUpService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<DTRLogFieldsSetUpDTO>>> GetAll()
        {
            var data = await _DTRLogFieldsSetUpService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTRLogFieldsSetUpDTO>> GetById(int id)
        {
            var data = await _DTRLogFieldsSetUpService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<DTRLogFieldsSetUpDTO>> Create([FromBody] DTRLogFieldsSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _DTRLogFieldsSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DTRLogFieldsSetUpDTO>> Update(int id, [FromBody] DTRLogFieldsSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _DTRLogFieldsSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _DTRLogFieldsSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
