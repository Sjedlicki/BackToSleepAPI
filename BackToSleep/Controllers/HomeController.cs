using BackToSleep.Models;
using Microsoft.AspNet.Identity;
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

        public List<string> UserSleepWeek()
        {
            string userID = User.Identity.GetUserId();
            List<SleepData> userSleep = db.SleepDatas.Where(s => s.UserID == userID).ToList();

            List<string> hours = (from hour in userSleep
                        select hour.SleepHours).ToList();

            return hours;
        }

        public int GetHours()
        {
           List<string> hours = UserSleepWeek();

            List<int> weekHours = new List<int>();

            foreach (string hr in hours)
            {
                weekHours.Add(int.Parse(hr));
            }

            int totalHours = weekHours.Sum();

            return totalHours;
        }

        public string SleepData(int hr)
        {
            //int hr = GetHours();

            List<SleepDB> sleep = db.SleepDBs.ToList();

            if (hr <= 2)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 2
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hr > 2 && hr < 6)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 4
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hr >= 6 && hr < 8 )
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 6
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hr >= 8 && hr < 10)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 8
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hr >= 10)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 10
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
            string YelpKey = SleepData(8);
            //int ZipCode = 48306;

            string lat = GPlacesDAL.GetLatitude(ZipCode);
            string lng = GPlacesDAL.GetLongitude(ZipCode);

            ViewBag.Business = GPlacesDAL.GetBusiness(lat, lng, YelpKey);

            ViewBag.Names = GPlacesDAL.GetName(ViewBag.Business);

            ViewBag.Images = GPlacesDAL.GetImage(ViewBag.Business);

            ViewBag.Links = GPlacesDAL.GetLink(ViewBag.Business);

            return View();
        }

        public ActionResult DailySleepPatterns()
        {
            return View();
        }

        public ActionResult DailySleep(int ZipCode, int SleepHours)
        {
            string YelpKey = SleepData(SleepHours);

            string lat = GPlacesDAL.GetLatitude(ZipCode);
            string lng = GPlacesDAL.GetLongitude(ZipCode);

            ViewBag.Business = GPlacesDAL.GetBusiness(lat, lng, YelpKey);

            ViewBag.Names = GPlacesDAL.GetName(ViewBag.Business);

            ViewBag.Images = GPlacesDAL.GetImage(ViewBag.Business);

            ViewBag.Links = GPlacesDAL.GetLink(ViewBag.Business);

            return View("GetLocation");
        }
    }
}