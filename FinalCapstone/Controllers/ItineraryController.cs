using FinalCapstone.Dal;
using FinalCapstone.Extensions;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace FinalCapstone.Controllers
{
    public class ItineraryController : Controller
    {
        private readonly IItineraryDAL _itineraryDAL;

        public ItineraryController(IItineraryDAL itineraryDAL)
        {
            _itineraryDAL = itineraryDAL;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult AddToItinerary(string id)
        {
            Landmark itineraryItem = _itineraryDAL.GetLandmark(id);
            itineraryItem = _itineraryDAL.GetLandmark(itineraryItem.Code);

            Itinerary itinerary = GetActiveItinerary();
            itinerary.AddToItinerary(itineraryItem);

            SaveActiveItinerary(itinerary);

            return RedirectToAction("ViewItinerary");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RemoveFromItinerary(string id)
        {
            var item = _itineraryDAL.GetLandmark(id);

            var itinerary = GetActiveItinerary();

            itinerary.RemoveOne(item);

            SaveActiveItinerary(itinerary);

            return RedirectToAction("ViewItinerary");
        }

        [HttpPost]
        public ActionResult DeleteItinerary()
        {
            DeleteActiveItinerary();
            return RedirectToAction("ViewItinerary");
        }

        [AllowAnonymous]
        public IActionResult ViewItinerary()
        {
            Itinerary itinerary = GetActiveItinerary();
            return View(itinerary);
        }

        private Itinerary GetActiveItinerary()
        {
            Itinerary itinerary = null;

            if (HttpContext.Session.Get<Itinerary>("Itinerary") == null)
            {
                itinerary = new Itinerary();
                SaveActiveItinerary(itinerary);
            }
            else
            {
                itinerary = HttpContext.Session.Get<Itinerary>("Itinerary");
            }
            return itinerary;
        }

        private void SaveActiveItinerary(Itinerary itinerary)
        {
            HttpContext.Session.Set("Itinerary", itinerary);
        }

        [Authorize]
        private void DeleteActiveItinerary()
        {
            HttpContext.Session.Remove("Itinerary");
        }

        [Authorize]
        public IActionResult SaveUserItinerary(Itinerary itinerary, string user)
        {
            itinerary = HttpContext.Session.Get<Itinerary>("Itinerary");
            user = HttpContext.User.Identity.Name;

            _itineraryDAL.SaveUserItinerary(itinerary, user);

            return RedirectToAction(nameof(SavedItineraries));
        }

        [Authorize]
        public IActionResult SavedItineraries(string user)
        {
            user = HttpContext.User.Identity.Name;

            var savedItineraries = _itineraryDAL.GetAllUserItineraries(user);

            return View(savedItineraries);
        }

        [Authorize]
        public IActionResult SavedItinerary(int id)
        {
            var savedItinerary = _itineraryDAL.GetUserItinerary(id);

            return View(savedItinerary);
        }

        [Authorize]
        public IActionResult DeleteSavedItinerary(int id)
        {
            _itineraryDAL.DeleteItinerary(id);

            return RedirectToAction(nameof(SavedItineraries));
        }
    }
}