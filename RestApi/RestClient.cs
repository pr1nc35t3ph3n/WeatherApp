using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace RestApi
{
   
    class RestClient : WeatherAPI
    {    
           
        // Web Request Methods.
        public enum httpverb
        {
            GET,
            PUT,
            POST,
            DELETE
        }

        private static string baseUrl = "http://api.openweathermap.org/data/2.5/weather?id=";

        private static string forecastUrl = "http://samples.openweathermap.org/data/2.5/forecast?";

        public RestClient()
        {
           
        }

        // Returns the Weather Forecast today.        
        public static WeatherAPI.RootObject makeRequest(string id)
        {
            string actionUrl = baseUrl + id + "&appid=98d8eb3d190051551f5cdbb079b6670d";
            HttpWebRequest request = WebRequest.CreateHttp(actionUrl);
            request.Method = httpverb.GET.ToString();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            object objResponse = reader.ReadToEnd();

            WeatherAPI.RootObject weat = JsonConvert.DeserializeObject<WeatherAPI.RootObject>(objResponse.ToString());
            response.Close();

            return weat;
        }

        // Five days forecast.
        public static FiveDaysForecast.RootObject fiveDaysForecast(string id)
        {
            FiveDaysForecast.RootObject fdf = new FiveDaysForecast.RootObject();
            string url = forecastUrl + id + "&appid=98d8eb3d190051551f5cdbb079b6670d";
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = httpverb.GET.ToString();
            var response = request.GetResponse();

            var dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            object objResponse = reader.ReadToEnd();

            fdf = JsonConvert.DeserializeObject<FiveDaysForecast.RootObject>(objResponse.ToString());
            response.Close();

            return fdf;
        }
       
    }
}
