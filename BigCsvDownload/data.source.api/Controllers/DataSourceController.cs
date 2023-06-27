using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace data.source.api.Controllers;

[ApiController]
[Route("[controller]")]
public class DataSourceController : ControllerBase
{
    [HttpGet]
    [Route("get/big-data")]
    public async Task DownloadFile()
    {
        var numberOfLines = 50000000;

        Response.ContentType = "text/csv";
        Response.Headers["Content-Disposition"] = "attachment; filename=output.csv";

        await foreach (var line in GenerateCsvLines(numberOfLines))
        {
            var buffer = Encoding.UTF8.GetBytes(line);
            await Response.Body.WriteAsync(buffer, 0, buffer.Length);
            await Response.Body.FlushAsync();
        }

    }

    private async IAsyncEnumerable<string> GenerateCsvLines(int numberOfLines)
    {
        // Add column headers
        var header = new StringBuilder();
        for (int i = 1; i <= 10; i++)
        {
            if (i > 1)
            {
                header.Append(',');
            }

            header.Append("column" + i);
        }
        yield return header.ToString() + Environment.NewLine;

        for (var i = 0; i < numberOfLines; i++)
        {
            var sb = new StringBuilder();
            for (var j = 0; j < 10; j++)
            {
                if (j > 0)
                {
                    sb.Append(',');
                }

                sb.Append(Guid.NewGuid());
            }

            yield return sb.ToString() + Environment.NewLine;
        }
    }
}
