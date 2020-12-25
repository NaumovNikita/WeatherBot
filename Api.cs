using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using SomeBot.Models;

namespace SomeBot
{
   public class Api
    {
         public static string apiLogic(string city)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&lang=ru&units=metric&appid={SomeBot.Config.apiToken}";

            var request = (HttpWebRequest)WebRequest.Create(url);

            var httpresponse = (HttpWebResponse)request.GetResponse();
            string response;
            using (var reader = new StreamReader(httpresponse.GetResponseStream()))
            {
                response = reader.ReadToEnd();
            }

            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

            var weatherText = $"В {weatherResponse.Name} {weatherResponse.Main.Temp}°C\n" +
                $"Максимальная {weatherResponse.Main.TempMax}°C и минимальная {weatherResponse.Main.TempMin}°C погода на сегодня\n" +
                $"Скорость ветра: {weatherResponse.Wind.Speed} метр/сек";
            return weatherText;
        }

    }
}
