using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Services.Interfaces.Import;


namespace WebbApp.Controllers.Import
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : ControllerBase
    {
        public ExcelController()
        {
            ExcelPackage.License.SetNonCommercialPersonal("128TCI");
        }

        [HttpPost("worksheets")]
        public async Task<IActionResult> GetWorksheets(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var worksheets = new List<WorksheetInfo>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var package = new ExcelPackage(stream))
                {
                    foreach (var worksheet in package.Workbook.Worksheets)
                    {
                        var dimension = worksheet.Dimension;

                        var info = new WorksheetInfo
                        {
                            Name = worksheet.Name,
                            Index = worksheet.Index,
                            RowCount = dimension?.Rows ?? 0,
                            ColumnCount = dimension?.Columns ?? 0
                        };

                        // Get first 5 rows as preview
                        if (dimension != null)
                        {
                            //int previewRows = Math.Min(5, dimension.Rows);
                            for (int row = 2; row <= dimension.Rows; row++)
                            {
                                var rowData = new List<string>();
                                for (int col = 1; col <= dimension.Columns; col++)
                                {
                                    rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? "");
                                }
                                info.PreviewData.Add(rowData);
                            }
                        }

                        worksheets.Add(info);
                    }
                }
            }

            return Ok(worksheets);
        }

        [HttpPost("worksheet-data")]
        public async Task<IActionResult> GetWorksheetData(IFormFile file, [FromQuery] string worksheetName)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[worksheetName];

                    if (worksheet == null)
                        return NotFound($"Worksheet '{worksheetName}' not found");

                    var dimension = worksheet.Dimension;
                    if (dimension == null)
                        return Ok(new { rows = 0, columns = 0, data = new List<object>() });

                    var data = new List<Dictionary<string, string>>();

                    // Assuming first row is header
                    var headers = new List<string>();
                    for (int col = 1; col <= dimension.Columns; col++)
                    {
                        headers.Add(worksheet.Cells[1, col].Value?.ToString() ?? $"Column{col}");
                    }

                    // Read data rows
                    for (int row = 2; row <= dimension.Rows; row++)
                    {
                        var rowData = new Dictionary<string, string>();
                        for (int col = 1; col <= dimension.Columns; col++)
                        {
                            rowData[headers[col - 1]] = worksheet.Cells[row, col].Value?.ToString() ?? "";
                        }
                        data.Add(rowData);
                    }

                    return Ok(new
                    {
                        worksheetName,
                        rows = dimension.Rows,
                        columns = dimension.Columns,
                        headers,
                        data
                    });
                }
            }
        }
    }
}
