using System;
using FinalCapstone.Dal;
using FinalCapstone.Dal.IDAL;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalCapstone.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewDAL _reviewDAL;
        private readonly ILandmarkDAL _landmarkDAL;
        private const string NewReviewResultKey = nameof(NewReviewResultKey);

        public ReviewController(IReviewDAL reviewDAL, ILandmarkDAL landmarkDAL)
        {
            _reviewDAL = reviewDAL;
            _landmarkDAL = landmarkDAL;
        }

        [HttpGet]
        public IActionResult NewReview(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewReview(Review model, string id)
        {
            model.ReviewDate = DateTime.UtcNow;
            model.Username = HttpContext.User.Identity.Name;
            model.AttractionCode = id;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _reviewDAL.SaveNewReview(model);

            TempData[NewReviewResultKey] = $"Thanks for sharing your experience, {model.Username}! Have an AMAZING day!";

            return RedirectToAction("Neighborhoods","Neighborhood");
        }
    }
}