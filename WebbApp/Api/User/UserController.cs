using DomainEntities.DTO.User;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.User;
using Services.Interfaces.Employee;

namespace WebbApp.Api.User;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _employeeService;

    public UserController(IUserService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetAll()
    {
        var employees = await _employeeService.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetById(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> Create([FromBody] UserDTO dto)
    {
        if (await _employeeService.UserNameExists(dto.UserName))
        {
            return Unauthorized("Email already exists.");
        }

        var result = await _employeeService.CreateAsync(dto);

        // REMOVED: CreatedAtAction
        // ADDED: Simple Ok result
        return Ok(result);
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult<UserDTO>> Update(int userId, [FromBody] UserDTO dto)
    {
        if (userId != dto.UserID)
            return BadRequest("ID mismatch");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _employeeService.UpdateAsync(dto);
        return Ok(result);
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult> Delete(int userId)
    {
        var success = await _employeeService.DeleteAsync(userId);
        if (!success)
            return NotFound();

        return NoContent();
    }
}