using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalCapstone.Models;
using FinalCapstone.Dal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace FinalCapstone.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Home()
        {
            return View();
        }

        private readonly INeighborhoodDAL _neighborhoodDAL;
        private IHostingEnvironment _env;

        public HomeController(INeighborhoodDAL neighborhoodDAL, IHostingEnvironment env)
        {
            _neighborhoodDAL = neighborhoodDAL;
            _env = env;
        }

        [AllowAnonymous]
        public IActionResult Index(NeighborhoodViewModel model)
        {
            IList<Neighborhood> neighborhoods = _neighborhoodDAL.GetNeighborhoodNames();
            model.Neighborhoods = neighborhoods;

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult UserImages()
        {
            var abs = Path.GetFullPath("~/wwwroot/userImages/").Replace("~\\", "");

            UploadImage model = new UploadImage()
            {
                Images = Directory.EnumerateFiles(abs)
                                  .Select(fn => Path.GetFileName(fn))
            };
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Map()
        {
            return View("Map");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
