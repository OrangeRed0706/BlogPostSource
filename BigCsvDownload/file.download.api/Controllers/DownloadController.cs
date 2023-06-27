using Microsoft.AspNetCore.Mvc;

namespace file.download.api.Controllers;

[ApiController]
[Route("[controller]")]
public class DownloadController : ControllerBase
{
    [HttpGet]
    [Route("api/download")]
    public async Task<IActionResult> DownloadFile()
    {
        var httpClient = new HttpClient();

        var url = "https://localhost:7124/DataSource/get-big-data/";

        var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode);
        }

        var stream = await response.Content.ReadAsStreamAsync();

        var fileStreamResult = new FileStreamResult(stream, "text/csv")
        {
            FileDownloadName = "output.csv"
        };

        return fileStreamResult;
    }
}
