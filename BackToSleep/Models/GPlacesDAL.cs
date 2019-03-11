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
        public static string GetLatitude(string UserAddress)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json";
            string ApiKey = ConfigurationManager.AppSettings["GoogleAPI"];
            HttpWebRequest request = WebRequest.CreateHttp($"{url}?address={UserAddress}&key={ApiKey}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();
            rd.Close();
            JObject JData = JObject.Parse(data);
            var Lat = JData["results"][0]["geometry"]["location"]["lat"].ToString();
            return Lat;
        }

        public static string GetLongitude(string UserAddress)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json";
            string ApiKey = ConfigurationManager.AppSettings["GoogleAPI"];
            HttpWebRequest request = WebRequest.CreateHttp($"{url}?address={UserAddress}&key={ApiKey}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();
            rd.Close();
            JObject JData = JObject.Parse(data);
            var LNG = JData["results"][0]["geometry"]["location"]["lng"].ToString();
            return LNG;
        }
        public static string GetPlace(string lat, string lng)
        {
            string url = $"https://api.yelp.com/v3/businesses/search?term=cafe&latitude={lat}&longitude={lng}";

            HttpWebRequest webRequest = WebRequest.CreateHttp(url);
           
            webRequest.Headers.Add("Authorization", $"Bearer {ConfigurationManager.AppSettings["YelpAPIKey"]}");

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            var stream = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            var content = stream.ReadToEnd();
            stream.Close();

            var Json = JObject.Parse(content);

            string business = Json["businesses"][0]["url"].ToString();

            return business;
        }
    }
}