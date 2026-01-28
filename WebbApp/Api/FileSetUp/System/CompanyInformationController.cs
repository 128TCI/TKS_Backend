using DomainEntities.DTO.FileSetUp.System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.FileSetUp.Employment;
using Services.Interfaces.FileSetUp.System;
using Services.Services.FileSetUp.System;

namespace WebbApp.Api.FileSetUp.System
{
    [Route("api/Fs/System/[controller]")]
    [ApiController]
    public class CompanyInformationController : ControllerBase
    {
        private readonly ICompanyInformationService _CompanyInformationService;

        public CompanyInformationController(ICompanyInformationService companyInformationService)
        {
            _CompanyInformationService = companyInformationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyInformationDTO>>> GetAll()
        {
            var employeeStatus = await _CompanyInformationService.GetAllAsync();
            return Ok(employeeStatus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyInformationDTO>> GetById(int id)
        {
            var employeeStatus = await _CompanyInformationService.GetByIdAsync(id);
            if (employeeStatus == null)
                return NotFound();

            return Ok(employeeStatus);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyInformationDTO>> Create([FromBody] CompanyInformationDTO companyInfo)  // CHANGED: dto → companyInfo
        {
            if (companyInfo == null)  // CHANGED: dto → companyInfo
            {
                return BadRequest("Company information is required");  // CHANGED: Better error message
            }

            var result = await _CompanyInformationService.CreateAsync(companyInfo);  // CHANGED: dto → companyInfo

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CompanyInformationDTO>> Update(int id, [FromBody] CompanyInformationDTO companyInfo)  // CHANGED: dto → companyInfo
        {
            if (id != companyInfo.CompanyID)  // CHANGED: dto → companyInfo
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _CompanyInformationService.UpdateAsync(companyInfo);  // CHANGED: dto → companyInfo
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _CompanyInformationService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}