using System;
using System.Collections.Generic;
using System.Diagnostics;
using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace FinalCapstone.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class NewLandmarkController : Controller
    {
        private readonly ILandmarkDAL _landmarkDAL;

        public NewLandmarkController(ILandmarkDAL landmarkDAL)
        {
            _landmarkDAL = landmarkDAL;
        }
       
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Landmark model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            else
            {
                _landmarkDAL.SaveLandmark(model);

                return RedirectToAction(nameof(SuccessfulAddition));
            }
        }

        public IActionResult SuccessfulAddition()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}