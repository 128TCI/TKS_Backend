
using Microsoft.EntityFrameworkCore;

namespace DomainEntities.Dto;
[Keyless]
public class DayTypeDto
{
    public string? EmpCode { get; set; }
    public string? DayType { get; set; }
}
