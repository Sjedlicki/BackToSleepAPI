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

        public int GetHours(List<string> hours)
        {
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

        public double GetQuality(List<int> quality)
        {
            List<int> weekQuality = new List<int>();

            foreach(int q in quality)
            {
                weekQuality.Add(q);
            }

            double averageQuality = weekQuality.Average();

            return averageQuality;
        }

        public int BasePoints(int basepoints)
        {
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


        public double AdjustedScore(int basepoints, double quality)
        {
            double q = quality * 0.1;

            double adjustedScore = basepoints * q;

            return adjustedScore;
        }

        public string SleepDaily(double sleepScore)
        {
            List<SleepDB> sleep = db.SleepDBs.ToList();

            if (sleepScore < 20)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 2
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (sleepScore >= 20 && sleepScore < 40)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 4
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (sleepScore >= 40 && sleepScore < 60)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 6
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (sleepScore >= 60 && sleepScore < 80)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 8
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (sleepScore >= 80)
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

        public double SleepWeeklyScore()
        {
            List<string> hours = UserSleepWeekHours();
            int avgSleepHours = GetHours(hours);
            int basepoints = BasePoints(avgSleepHours);

            List<int> avgSleepQuality = UserSleepWeekQuality();
            double quality = GetQuality(avgSleepQuality);

            double adj = AdjustedScore(basepoints, quality);

            return adj;             
        }

        public string SleepWeekly()
        {
            double hr = SleepWeeklyScore();

            List<SleepDB> sleep = db.SleepDBs.ToList();

            if (hr < 20)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 2
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hr >= 20  && hr < 40)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 4
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hr >= 40 && hr < 60)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 6
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hr >= 60 && hr < 80)
            {
                string YelpKey = (from yelp in sleep
                                  where yelp.SleepHours == 8
                                  select yelp.YelpKey).First();

                return YelpKey;
            }
            else if (hr >= 80)
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

        public ActionResult DailySleep(int ZipCode, int SleepHours, int SleepQuality)
        {
            int baseScore = BasePoints(SleepHours);
            double adjusted = AdjustedScore(baseScore, SleepQuality);

            string YelpKey = SleepDaily(adjusted);

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