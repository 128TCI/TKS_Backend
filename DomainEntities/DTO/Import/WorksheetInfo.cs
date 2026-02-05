
using Microsoft.AspNetCore.Http;

namespace DomainEntities.Dto;
public class WorksheetInfo
{
    public string Name { get; set; }
    public int Index { get; set; }
    public int RowCount { get; set; }
    public int ColumnCount { get; set; }
    public List<List<string>> PreviewData { get; set; } = new();
}
