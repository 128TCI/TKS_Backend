using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Services.Extensions;

namespace WebApp.Api;

[Route("api/downloads/[action]")]
[ApiController]
[Produces("application/json")]
public class DownloadController(IWebHostEnvironment env
    ) : ControllerBase
{
	private readonly IWebHostEnvironment _env = env;

    [HttpGet]
    public IActionResult DownloadTemplate(string filename)
    {
        if (filename.IsStringNullOrEmpty()) throw new Exception("filename is required");

        var filePath = Path.Combine(_env.ContentRootPath, "Files", "Templates", filename);
        var fileInfo = new FileInfo(filePath);
        new FileExtensionContentTypeProvider().TryGetContentType(fileInfo.FullName, out string? contentType);
        if (contentType.IsStringNullOrEmpty())
        {
            throw new Exception("Content Type Cannot be parsed.");
        }
        return PhysicalFile(fileInfo.FullName, contentType!, fileInfo.Name);
    }
}