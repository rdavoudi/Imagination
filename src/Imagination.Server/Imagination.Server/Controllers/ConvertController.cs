using Imagination.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Imagination.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly ILogger<ConvertController> _logger;
        public ConvertController(ILogger<ConvertController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            await using var file = new MemoryStream();
            try
            {
                _logger.LogInformation("Process started");

                await Request.Body.CopyToAsync(file);

                _logger.LogInformation("File received");

                var changeFileResponse = await ConvertToJpeg(file);


                if (changeFileResponse.Code == HttpStatusCode.OK)
                {
                    return GenerateFile(changeFileResponse.Stream);
                }

                return BadRequest(changeFileResponse.Message);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            finally
            {
                await file.FlushAsync();
            }
        }

        private async Task<ResultResponse> ConvertToJpeg(MemoryStream currentFile)
        {
            _logger.LogInformation("Converting Started");
            if (currentFile.Length == 0)
                return new ResultResponse("The file is empty.");

            using var img = Image.FromStream(currentFile);

            if (img.Width <= 0)
                return new ResultResponse("It's not a valid image file.");

            using var outStream = new MemoryStream();
            img.Save(outStream, ImageFormat.Jpeg);

            _logger.LogInformation("Converting Done");
            img.Dispose();
            await outStream.FlushAsync();

            return new ResultResponse(outStream);

        }

        private FileContentResult GenerateFile(MemoryStream stream)
        {
            _logger.LogInformation("Generated converted file Started");
            HttpContext.Response.ContentType = "image/jpeg";

            var result = new FileContentResult(stream.ToArray(), "image/jpeg");
            _logger.LogInformation("Process done.");
            return result;
        }
    }
}
