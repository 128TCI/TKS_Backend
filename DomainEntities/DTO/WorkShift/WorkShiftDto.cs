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
