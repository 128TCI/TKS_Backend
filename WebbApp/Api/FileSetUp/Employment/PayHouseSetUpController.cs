using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;

namespace WebbApp.Api.FileSetUp.Employment
{
    [Route("api/Fs/Employment/[controller]")]
    [ApiController]
    public class PayHouseSetUpController : ControllerBase
    {
    private readonly IPayHouseSetUpService _PayHouseSetUpService;

    public PayHouseSetUpController(IPayHouseSetUpService payHouseSetUpService)
    {
        _PayHouseSetUpService = payHouseSetUpService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PayHouseSetUpDTO>>> GetAll()
    {
        var employeeStatus = await _PayHouseSetUpService.GetAllAsync();
        return Ok(employeeStatus);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PayHouseSetUpDTO>> GetById(int id)
    {
        var employeeStatus = await _PayHouseSetUpService.GetByIdAsync(id);
        if (employeeStatus == null)
            return NotFound();

        return Ok(employeeStatus);
    }

    [HttpPost]
    public async Task<ActionResult<PayHouseSetUpDTO>> Create([FromBody] PayHouseSetUpDTO dto)
    {
        if (dto == null)
        {
            return Unauthorized("null");
        }

        var result = await _PayHouseSetUpService.CreateAsync(dto);

        // REMOVED: CreatedAtAction
        // ADDED: Simple Ok result
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PayHouseSetUpDTO>> Update(int id, [FromBody] PayHouseSetUpDTO dto)
    {
        if (id != dto.LineID)
            return BadRequest("ID mismatch");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _PayHouseSetUpService.UpdateAsync(dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await _PayHouseSetUpService.DeleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
}
