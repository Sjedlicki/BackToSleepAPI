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
        private SleeperDbContext db = new SleeperDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public string SleepData(int hours)
        {

            List<SleepDB> sleep = db.SleepDBs.ToList();

            if (hours <= 2)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 2
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hours > 2 && hours < 6)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 4
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hours >= 6 && hours < 8 )
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 6
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hours >= 8 && hours < 10)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 8
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hours >= 10)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 8
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else
            {
                return null;
            }
        }

        public ActionResult GetLocation(int ZipCode)
        {
            int hours = 3;

            string YelpKey = SleepData(hours);

            string lat = GPlacesDAL.GetLatitude(ZipCode);
            string lng = GPlacesDAL.GetLongitude(ZipCode);

            ViewBag.Business = GPlacesDAL.GetBusiness(lat, lng, YelpKey);

            ViewBag.Names = GPlacesDAL.GetName(ViewBag.Business);

            ViewBag.Images = GPlacesDAL.GetImage(ViewBag.Business);

            ViewBag.Links = GPlacesDAL.GetLink(ViewBag.Business);

            return View();

        }
    }
}