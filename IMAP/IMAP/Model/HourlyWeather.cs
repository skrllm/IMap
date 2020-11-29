using System;
using System.Collections.Generic;
using System.Text;

namespace IMAP.Model
{
    public class HourlyWeather
    {
        public string Temperature { get; set; }
        public string Time { get; set; }
        public string NameTime { get; set; }

        public int MaxWindSpeed { get; set; }
        
        public double RainFall { get; set; }
    }
}
