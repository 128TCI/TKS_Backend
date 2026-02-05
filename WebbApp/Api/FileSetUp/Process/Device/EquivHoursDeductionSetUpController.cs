using DomainEntities.DTO.FileSetUp.Process.Device;
using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process.Device;
using Services.Interfaces.FileSetUp.Process.Device.EquivHoursDeductionSetUp;

namespace WebbApp.Api.FileSetUp.Process.Device
{
    [Route("api/Fs/Process/Device/[controller]")]
    [ApiController]
    public class EquivHoursDeductionSetUpController : ControllerBase
    {
        private readonly IEquivDayForAbsentService _EquivDayForAbsentService;
        private readonly IEquivDayForNoLoginService _EquivDayForNoLoginService;
        private readonly IEquivDayForNoLogoutService _EquivDayForNoLogoutService;
        private readonly IEquivDayForNoBreak2InService _EquivDayForNoBreak2InService;
        private readonly IEquivDayForNoBreak2OutService _EquivDayForNoBreak2OutService;

        public EquivHoursDeductionSetUpController(
            IEquivDayForAbsentService service,
            IEquivDayForNoLoginService equivDayForNoLoginService,
            IEquivDayForNoLogoutService equivDayForNoLogoutService,
            IEquivDayForNoBreak2InService equivDayForNoBreak2InService,
            IEquivDayForNoBreak2OutService equivDayForNoBreak2OutService

            )
        {
            _EquivDayForAbsentService = service;
            _EquivDayForNoLoginService = equivDayForNoLoginService;
            _EquivDayForNoLogoutService = equivDayForNoLogoutService;
            _EquivDayForNoBreak2InService = equivDayForNoBreak2InService;
            _EquivDayForNoBreak2OutService = equivDayForNoBreak2OutService;
        }
        [HttpGet("ForAbsent")]
        public async Task<ActionResult<List<EquivDayForAbsentDTO>>> GetAll()
        {
            var data = await _EquivDayForAbsentService.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("ForAbsent{id}")]
        public async Task<ActionResult<EquivDayForAbsentDTO>> GetById(int id)
        {
            var data = await _EquivDayForAbsentService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }
        [HttpPost("ForAbsent")]
        public async Task<ActionResult<EquivDayForAbsentDTO>> Create([FromBody] EquivDayForAbsentDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _EquivDayForAbsentService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }
        [HttpPut("ForAbsent{id}")]
        public async Task<ActionResult<EquivDayForAbsentDTO>> Update(int id, [FromBody] EquivDayForAbsentDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _EquivDayForAbsentService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("ForAbsent{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _EquivDayForAbsentService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
        //ForNoLogin
        [HttpGet("ForNoLogin")]
        public async Task<ActionResult<List<EquivDayForNoLoginDTO>>> GetAllForNoLogin()
        {
            var data = await _EquivDayForNoLoginService.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("ForNoLogin{id}")]
        public async Task<ActionResult<EquivDayForNoLoginDTO>> GetByIdForNoLogin(int id)
        {
            var data = await _EquivDayForNoLoginService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }
        [HttpPost("ForNoLogin")]
        public async Task<ActionResult<EquivDayForNoLoginDTO>> CreateForNoLogin([FromBody] EquivDayForNoLoginDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _EquivDayForNoLoginService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }
        [HttpPut("ForNoLogin{id}")]
        public async Task<ActionResult<EquivDayForNoLoginDTO>> UpdateForNoLo(int id, [FromBody] EquivDayForNoLoginDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _EquivDayForNoLoginService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("ForNoLogin{id}")]
        public async Task<ActionResult> DeleteForNoLogin(int id)
        {
            var success = await _EquivDayForNoLoginService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
        //ForNoLogout
        [HttpGet("ForNoLogout")]
        public async Task<ActionResult<List<EquivDayForNoLogoutDTO>>> GetAllForNoLogout()
        {
            var data = await _EquivDayForNoLogoutService.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("ForNoLogout{id}")]
        public async Task<ActionResult<EquivDayForNoLogoutDTO>> GetByIdForNoLogout(int id)
        {
            var data = await _EquivDayForNoLogoutService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }
        [HttpPost("ForNoLogout")]
        public async Task<ActionResult<EquivDayForNoLogoutDTO>> CreateForNoLogout([FromBody] EquivDayForNoLogoutDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _EquivDayForNoLogoutService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }
        [HttpPut("ForNoLogout{id}")]
        public async Task<ActionResult<EquivDayForNoLogoutDTO>> UpdateForNoLogout(int id, [FromBody] EquivDayForNoLogoutDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _EquivDayForNoLogoutService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("ForNoLogout{id}")]
        public async Task<ActionResult> DeleteForNoLogout(int id)
        {
            var success = await _EquivDayForNoLogoutService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
        //ForNoBreak2In
        [HttpGet("ForNoBreak2In")]
        public async Task<ActionResult<List<EquivDayForNoBreat2InDTO>>> GetAllForNoBreak2In()
        {
            var data = await _EquivDayForNoBreak2InService.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("ForNoBreak2In{id}")]
        public async Task<ActionResult<EquivDayForNoBreat2InDTO>> GetByIdForNoBreak2In(int id)
        {
            var data = await _EquivDayForNoBreak2InService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }
        [HttpPost("ForNoBreak2In")]
        public async Task<ActionResult<EquivDayForNoBreat2InDTO>> CreateForNoBreak2In([FromBody] EquivDayForNoBreat2InDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _EquivDayForNoBreak2InService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }
        [HttpPut("ForNoBreak2In{id}")]
        public async Task<ActionResult<EquivDayForNoBreat2InDTO>> UpdateForNoBreak2In(int id, [FromBody] EquivDayForNoBreat2InDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _EquivDayForNoBreak2InService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("ForNoBreak2In{id}")]
        public async Task<ActionResult> DeleteForNoBreak2In(int id)
        {
            var success = await _EquivDayForNoBreak2InService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
