using System;
using System.Net;
using System.Web.Script.Services;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;


namespace PalashChakma_eTaskr
{
    public partial class Default : System.Web.UI.Page
    {
        public static string _latitude, _longitude;
        private const string APIKEY = "45129e9b4979716858592bdd54265f5b";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public static void GetWeatherReport(string lat,string lng)
        {
            weatherLatLng.Lat = lat;
            weatherLatLng.Lng = lng;           
        }

        protected void btnGetWeather_Click(object sender, EventArgs e)
        {
            string _lat = weatherLatLng.Lat;
            string _lng = weatherLatLng.Lng;

            string jsonWeather = string.Empty;
            

            string url = @"https://api.forecast.io/forecast/" + APIKEY + @"/" + _lat + "," + _lng;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())

            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonWeather = reader.ReadToEnd();
                dynamic stuff = JObject.Parse(jsonWeather);
                string temperaure = stuff.currently.temperature;
                //set temperature                
                string ctemp = FarenheightToDegree(temperaure);
                if (ctemp != "error")
                {
                    lblcurrtemp.Text = ctemp;
                }
                else
                {
                    lblcurrtemp.Text = "error while fetching temperature. please try later.";
                }

                string sticon = stuff.currently.icon;
                SetWeatherIcon(sticon);
            }
        }
        private void SetWeatherIcon(string sweathericon)
        {
            switch (sweathericon)
            {
                case "clear-day":
                    imgWeather.ImageUrl = "images/sunny.png";
                    break;
                case "clear-night":
                    imgWeather.ImageUrl = "images/clear_night.png";
                    break;
                case "rain":
                    imgWeather.ImageUrl = "images/rain.png";
                    break;
                case "snow":
                    imgWeather.ImageUrl = "images/snow.png";
                    break;
                case "sleet":
                    imgWeather.ImageUrl = "images/snow.png";
                    break;
                case "wind":
                    imgWeather.ImageUrl = "images/wind.png";
                    break;
                case "fog":
                    imgWeather.ImageUrl = "images/fog.png";
                    break;
                case "cloudy":
                    imgWeather.ImageUrl = "images/cloudy.png";
                    break;
                case "partly-cloudy-day":
                    imgWeather.ImageUrl = "images/partly_cloudy_day.png";
                    break;
                case "partly-cloudy-night":
                    imgWeather.ImageUrl = "images/cloudy_night.png";
                    break;
                default:
                    break;
            }
        }
        private string FarenheightToDegree(string farenh)
        {
            try
            { 
            decimal dmal = System.Convert.ToDecimal(farenh);
            var deg = (dmal - 32) * 5 / 9;
            string fmtdeg = String.Format("{0:0.0#}", deg);
            return fmtdeg;
            }
            catch(Exception ex)
            {
                return "error";
            }
        }

        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void GetWeather(string latitude, string longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
            GetWeatherReport(_latitude,_longitude);
        }

    }
}