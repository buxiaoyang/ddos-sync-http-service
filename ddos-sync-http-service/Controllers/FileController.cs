using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using aspnetapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "File");
            string[] temp = id.Replace("__",".").Split('_');
            string fullPath = Path.Combine(rootPath, temp[0], temp[1]);
            FileStream stream = System.IO.File.OpenRead(fullPath);

            if (stream == null)
                return NotFound();

            return File(stream, "application/octet-stream");
        }

        // GET: api/File/
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult File()
        {
            try
            {
                var file = Request.Form.Files[0];
                string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "File");

                if (file.Length > 0)
                {
                    var fileExtension = Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
                    var fileName = Path.Combine(DateTime.Now.ToString("yyyy-MM-dd") ,Guid.NewGuid() + fileExtension);
                    var fullPath = Path.Combine(rootPath, fileName);
                    if (!Directory.Exists(Path.GetDirectoryName(fullPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                    }
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var fileNameParsed = fileName.Replace("\\", "_").Replace("/", "_").Replace(".", "__");
                    return Ok(new { fileNameParsed });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
