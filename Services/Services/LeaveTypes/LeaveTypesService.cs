using DomainEntities.Dto;
using Infrastructure.IRepositories.LeaveTypes;
using Services.Interfaces.LeaveTypes;

namespace Services.Services.LeaveTypes
{
    public class LeaveTypesService(ILeaveTypesRepository repository) : ILeaveTypesService
    {
        private readonly ILeaveTypesRepository _repository = repository;
        public async Task<LeaveTypesDto?> GetLeaveTypes(string leaveCode)
        {
            var code = await _repository.GetLeaveTypeByCode(leaveCode);
            return code;
        }
    }
}
