using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace BackToSleep.Models
{
    public class GPlacesDAL
    {
        public static string GetLatitude(int zipCode)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json";
            string ApiKey = ConfigurationManager.AppSettings["GoogleAPI"];

            HttpWebRequest request = WebRequest.CreateHttp($"{url}?address={zipCode}&key={ApiKey}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();
            rd.Close();

            JObject JData = JObject.Parse(data);
            var Lat = JData["results"][0]["geometry"]["location"]["lat"].ToString();

            return Lat;
        }

        public static string GetLongitude(int zipCode)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json";
            string ApiKey = ConfigurationManager.AppSettings["GoogleAPI"];

            HttpWebRequest request = WebRequest.CreateHttp($"{url}?address={zipCode}&key={ApiKey}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();
            rd.Close();

            JObject JData = JObject.Parse(data);
            var LNG = JData["results"][0]["geometry"]["location"]["lng"].ToString();

            return LNG;
        }

        public static List<string> GetBusiness(string lat, string lng, string YelpKeyword)
        {
            string url = $"https://api.yelp.com/v3/businesses/search?term={YelpKeyword}&radius=8000&latitude={lat}&longitude={lng}&sort_by=rating";

            HttpWebRequest webRequest = WebRequest.CreateHttp(url);

            webRequest.Headers.Add("Authorization", $"Bearer {ConfigurationManager.AppSettings["YelpAPIKey"]}");

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            var stream = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            var content = stream.ReadToEnd();
            stream.Close();

            var Json = JObject.Parse(content);

            List<string> businesses = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                string business = Json["businesses"][i]["id"].ToString();
                businesses.Add(business);
            }

            return businesses;
        }

        public static List<string> GetName(List<string> ids)
        {
            List<string> names = new List<string>();
            foreach (string id in ids)
            {
                string url = $"https://api.yelp.com/v3/businesses/{id}";

                HttpWebRequest webRequest = WebRequest.CreateHttp(url);

                webRequest.Headers.Add("Authorization", $"Bearer {ConfigurationManager.AppSettings["YelpAPIKey"]}");

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                var stream = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
                var content = stream.ReadToEnd();
                stream.Close();

                var Json = JObject.Parse(content);

                string name = Json["name"].ToString();

                names.Add(name);
            }

            return names;
        }

        public static List<string> GetImage(List<string> ids)
        {
            List<string> images = new List<string>();
            foreach (string id in ids)
            {
                string url = $"https://api.yelp.com/v3/businesses/{id}";

                HttpWebRequest webRequest = WebRequest.CreateHttp(url);

                webRequest.Headers.Add("Authorization", $"Bearer {ConfigurationManager.AppSettings["YelpAPIKey"]}");

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                var stream = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
                var content = stream.ReadToEnd();
                stream.Close();

                var Json = JObject.Parse(content);

                string image = Json["image_url"].ToString();

                images.Add(image);
            }

            return images;
        }

        public static List<string> GetLink(List<string> ids)
        {
            List<string> links = new List<string>();
            foreach (string id in ids)
            {
                string url = $"https://api.yelp.com/v3/businesses/{id}";

                HttpWebRequest webRequest = WebRequest.CreateHttp(url);

                webRequest.Headers.Add("Authorization", $"Bearer {ConfigurationManager.AppSettings["YelpAPIKey"]}");

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                var stream = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
                var content = stream.ReadToEnd();
                stream.Close();

                var Json = JObject.Parse(content);

                string link = Json["url"].ToString();

                links.Add(link);
            }

            return links;
        }
    }
}