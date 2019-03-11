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
            int zipCode = 48309;
            string lat = GPlacesDAL.GetLatitude(zipCode);
            string lng = GPlacesDAL.GetLongitude(zipCode);

            ViewBag.Business = GPlacesDAL.GetBusiness(lat, lng);

            ViewBag.Names = GPlacesDAL.GetName(ViewBag.Business);

            ViewBag.Images = GPlacesDAL.GetImage(ViewBag.Business);

            ViewBag.Links = GPlacesDAL.GetLink(ViewBag.Business);

            return View();
        }
    }
}