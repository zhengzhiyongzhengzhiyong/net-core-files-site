using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace Source_web.Controllers
{
    public class AttachmentsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        public AttachmentsController(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> PostFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");

                var uniqueFileName = GetUniqueFileName(file.FileName);
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                var path = Path.Combine(hostingEnvironment.ContentRootPath, "uploads", date, uniqueFileName);

                //var filePath = Path.GetTempFileName();
                //var fileName = Path.GetTempFileName();

                if (file.Length > 0)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                return Ok(new { uniqueFileName, path });
            }
            catch (Exception exp)
            {
                string message = $"file / upload failed!";
                return Json(message);
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        [HttpPost]
        public async Task<IActionResult> PostFiles(ICollection<IFormFile> files)
        {
            try
            {
                System.Console.WriteLine("You received the call!");
                WriteLog("PostFiles call received!", true);
                long size = files.Sum(f => f.Length);

                var filePath = Path.GetTempFileName();
                var fileName = Path.GetTempFileName();

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                return Ok(new { count = files.Count, fileName, size, filePath });
            }
            catch (Exception exp)
            {
                System.Console.WriteLine("Exception generated when uploading file - " + exp.Message);
                WriteLog("Exception generated when uploading file - " + exp.Message, true);
                string message = $"file / upload failed!";
                return Json(message);
            }
        }

        public void WriteLog(string Message, bool InsertNewLine)
        {
        }
    }
}