using System;
using System.Collections.Generic;
using System.Text;

namespace SomeBot.Models
{
    class WeatherResponse
    {
        public TemperatureInformation Main { get; set; }
        public string Name { get; set; }
        public WindInformation Wind { get; set; }
     
    }
}
