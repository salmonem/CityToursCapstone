using System;
using System.Collections.Generic;
using System.Diagnostics;
using FinalCapstone.Dal;
using FinalCapstone.Dal.IDAL;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalCapstone.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationDAL _radiusDAL;

        public LocationController(ILocationDAL radiusDAL)
        {
            _radiusDAL = radiusDAL;
        }

        public double GeoLoc { get; private set; }
        public decimal Longitude { get; private set; }
        public decimal Latitude { get; private set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Locations(string id)
        {
            string name;
            double latitude;
            double longitude;

            switch (id)
            {
                case "SN":
                    name = "The Short North";
                    latitude = 39.977133;
                    longitude = -83.004007;
                    break;
                case "PN":
                    name = "Polaris Fashion Place";
                    latitude = 40.145375;
                    longitude = -82.981868;
                    break;
                case "EA":
                    name = "Easton";
                    latitude = 40.050879;
                    longitude = -82.914202;
                    break;
                case "OSU":
                    name = "The OSU Stadium";
                    latitude = 40.014191;
                    longitude = -83.030914;
                    break;
                case "OT":
                    name = "The Ohio Theater";
                    latitude = 41.494549;
                    longitude = -81.795395;
                    break;
                case "SH":
                    name = "The Ohio Statehouse";
                    latitude = 39.961347;
                    longitude = -82.998830;
                    break;
                default:
                    name = "Columbus, OH";
                    latitude = 39.961178;
                    longitude = -82.998795;
                    break;
            }
            IList<Location> Locations = _radiusDAL.LocationsInRadius(name, latitude, longitude);

            return View(Locations);
        }
    }
}


