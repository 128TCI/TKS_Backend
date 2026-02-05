using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities.Dto;
public class WorkShiftDto
{
    [Key]
    //public int WorkShiftID { get; set; }
    public string? WorkShiftCode { get; set; }
    public string? WorkShiftDesc { get; set; }
}

[Keyless]
public class WorkShiftAsOfDateDto
{
    public string? WorkShiftCode { get; set; }
    public string? WorkShiftDesc { get; set; }
    public DateTime? TimeIn { get; set; }
    public DateTime? TimeOut { get; set; }
    public DateTime? Break1In { get; set; }
    public DateTime? Break1Out { get; set; }
    public DateTime? Break2In { get; set; }
    public DateTime? Break2Out { get; set; }
    public DateTime? Break3In { get; set; }
    public DateTime? Break3Out { get; set; }
    
}

[Keyless]
public class WorkShiftByInOrOut
{
    public string? WorkShiftCode { get; set; }
    public string? WorkShiftDesc { get; set; }
    public DateTime? TimeIn { get; set; }
    public DateTime? TimeOut { get; set; }
    public DateTime? Break1In { get; set; }
    public DateTime? Break1Out { get; set; }
    public DateTime? Break2In { get; set; }
    public DateTime? Break2Out { get; set; }
    public DateTime? Break3In { get; set; }
    public DateTime? Break3Out { get; set; }
    public string? WorkShiftType { get; set; }
    public bool Shift12AM { get; set; }

}