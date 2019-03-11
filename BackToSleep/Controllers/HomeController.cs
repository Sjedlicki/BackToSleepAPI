using BackToSleep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BackToSleep.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("GetLocation");
        }


        public ActionResult GetLocation()
        {            
            string city = "Warren";
            string state = "Michigan";
            ViewBag.Location = GetData(GPlacesDAL.GetLatitude($"{city}+{state}"), GPlacesDAL.GetLongitude($"{city}+{state}"));
            return View();
        }

        public static string GetData(string lat, string lng)
        {
            return GPlacesDAL.GetPlace(lat, lng);
        }

    }
}