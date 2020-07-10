using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Source_Api.Controllers
{
    /// <summary>
    /// 文件
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IHostingEnvironment hostingEnvironment;
        public FilesController(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");

                var uniqueFileName = GetUniqueFileName(file.FileName);
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                var dirpath = Path.Combine(hostingEnvironment.ContentRootPath, "uploads", date);

                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }

                var filePath = Path.Combine(dirpath, uniqueFileName);

                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                string webfileurl = $"http://localhost:58444/uploads/{date}/{uniqueFileName}";

                return Ok(new { path=webfileurl});
            }
            catch (Exception exp)
            {
                string message = $"file / upload failed!";
                return Ok(message);
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DownLoad(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");

                var uniqueFileName = GetUniqueFileName(file.FileName);
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                var dirpath = Path.Combine(hostingEnvironment.ContentRootPath, "uploads", date);

                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }

                var filePath = Path.Combine(dirpath, uniqueFileName);

                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }



                string webfileurl = $"http://localhost:58444/uploads/{date}/{uniqueFileName}";

                return Ok(new { path = webfileurl });
            }
            catch (Exception exp)
            {
                string message = $"file / upload failed!";
                return Ok(message);
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Guid.NewGuid().ToString() + Path.GetExtension(fileName);
        }
    }
}