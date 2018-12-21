using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace FinalCapstone.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _environment;

        // Constructor
        public UploadController(IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name, string id)
        {
            var newFileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "userImages") + $@"\{id}-{newFileName}";

                        // if you want to store path of folder in database
                        PathDB = "userImages/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }


            }
            return RedirectToAction("Neighborhoods","Neighborhood");
        }
    }
}