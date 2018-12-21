using System;
using System.Collections.Generic;
using System.Diagnostics;
using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalCapstone.Controllers
{
    public class NeighborhoodController : Controller
    {
        private readonly INeighborhoodDAL _neighborhoodDAL;
        private const string NewReviewResultKey = nameof(NewReviewResultKey);

        public NeighborhoodController(INeighborhoodDAL neighborhoodDAL)
        {
            _neighborhoodDAL = neighborhoodDAL;
        }

        public double GeoLoc { get; private set; }
        public decimal Longitude { get; private set; }
        public decimal Latitude { get; private set; }

        public IActionResult Neighborhoods(NeighborhoodViewModel model)
        {
            if (TempData.ContainsKey(NewReviewResultKey))
            {
                model.NewReviewResult = TempData[NewReviewResultKey] as string;
            }

            IList<Neighborhood> neighborhoods = _neighborhoodDAL.GetNeighborhoodNames();
            model.Neighborhoods = neighborhoods;

            return View(model);
        }

    }
}

