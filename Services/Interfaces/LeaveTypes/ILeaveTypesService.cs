
using DomainEntities.Dto;
using Services.DTOs;
using Services.DTOs.FileSetup.Employment;


namespace Services.Interfaces.LeaveTypes;

public interface ILeaveTypesService
{
    Task<LeaveTypesDto?> GetLeaveTypes(string leaveCode);
}

