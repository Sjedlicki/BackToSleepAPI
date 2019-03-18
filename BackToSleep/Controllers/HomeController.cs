using BackToSleep.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public List<string> UserSleepWeekHours()
        {
            string userID = User.Identity.GetUserId();
            List<SleepData> userSleep = db.SleepDatas.Where(s => s.UserID == userID).ToList();

            List<string> hours = (from hour in userSleep
                        select hour.SleepHours).ToList();

            return hours;
        }

        public int GetHours()
        {
           List<string> hours = UserSleepWeekHours();

            List<int> weekHours = new List<int>();

            foreach (string hr in hours)
            {
                weekHours.Add(int.Parse(hr));
            }

            double totalHours = weekHours.Average();

            int rounded = (int)Math.Round(totalHours, MidpointRounding.AwayFromZero);

            return rounded;
        }

        public List<int> UserSleepWeekQuality()
        {
            string userID = User.Identity.GetUserId();
            List<SleepData> userSleep = db.SleepDatas.Where(s => s.UserID == userID).ToList();

            List<int> quality = (from sq in userSleep
                                  select sq.SleepQuality).ToList();

            return quality;
        }

        public double GetQuality()
        {
            List<int> quality = UserSleepWeekQuality();

            List<int> weekQuality = new List<int>();

            foreach(int q in quality)
            {
                weekQuality.Add(q);
            }

            double averageQuality = weekQuality.Average();

            return averageQuality;
        }

        public int BasePoints()
        {
            int basepoints = GetHours();

            if (basepoints >= 8)
            {
                return 100;
            }
            else if (basepoints < 8 && basepoints >= 6)
            {
                return  80;
            }
            else if (basepoints < 6 && basepoints >= 4)
            {
                return 60;
            }
            else if (basepoints < 4 && basepoints >= 2)
            {
                return 40;
            }
            else if (basepoints < 2 && basepoints >= 0)
            {
                return 20;
            }

            return 0;
        }

        public double AdjustedDailyScore(int quality)
        {
            int bp = BasePoints();
            double q = quality * 0.1;

            double adjustedScore = bp * q;

            return adjustedScore;
        }

        public double AdjustedWeeklyScore()
        {
            int bp = BasePoints();
            double quality = GetQuality();

            double q = quality * 0.1;

            double adjustedScore = bp * q;

            return adjustedScore;
        }

        public string SleepDaily(int hr)
        {
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

        public string SleepWeekly()
        {
            double hr = GetHours();

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
            else if (hr >= 6 && hr < 8)
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

        [Authorize]
        public ActionResult GetLocation(int ZipCode)
        {
            string YelpKey = SleepWeekly();

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
            string YelpKey = SleepDaily(SleepHours);

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