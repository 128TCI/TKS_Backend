
using Microsoft.AspNetCore.Http;

namespace DomainEntities.Dto;
public class ExcelImportDto
{
    public IFormFile? File { get; set; }
    public string? SelectedWorksheet { get; set; }
}
