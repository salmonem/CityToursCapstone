using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using FinalCapstone.Dal.IDAL;

namespace FinalCapstone.Controllers
{
    [AllowAnonymous]
    public class LandmarkController : Controller
    {
        private readonly ILandmarkDAL _landmarkDAL;
        private readonly IReviewDAL _reviewDAL;
        private readonly IVoteDAL _voteDAL;

        public LandmarkController(ILandmarkDAL landmarkDAL, IReviewDAL reviewDAL, IVoteDAL voteDAL)
        {
            _landmarkDAL = landmarkDAL;
            _reviewDAL = reviewDAL;
            _voteDAL = voteDAL;
        }

        public IActionResult Index(string id)
        {
            IList<Landmark> neighboodLandmarks = _landmarkDAL.GetNeighborhoodLandmarks(id);

            return View(neighboodLandmarks);
        }

        public IActionResult Detail(string id, DisplayReviewsViewModel model)
        {

            IList<Landmark> landMarkDetail = _landmarkDAL.GetLandmark(id);
            IList<Review> reviews = _reviewDAL.GetAllReviews(id);

            model.Landmarks = landMarkDetail;
            model.Reviews = reviews;

            var abs = Path.GetFullPath("~/wwwroot/userImages/").Replace("~\\", "");

            model.Images = Directory.EnumerateFiles(abs)
                                .Select(fn => Path.GetFileName(fn));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Vote(Vote model, string id)
        {
            var vote = model.Value;
            model.Username = HttpContext.User.Identity.Name;
            model.AttractionCode = id;
            _voteDAL.SaveVote(model);

            return RedirectToAction("Neighborhoods", "Neighborhood");
        }

        public IActionResult Delete (string id)
        {
            _landmarkDAL.DeleteLandmark(id);
            return RedirectToAction("Neighborhoods", "Neighborhood");
        }

    }
}