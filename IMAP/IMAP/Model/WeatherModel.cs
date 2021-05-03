using System;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace IMAP.Model
{
    public class WeatherModel
    {
        public string Temperature { get; set; }

        public string Time { get; set; }

        public string IconURL { get; set; }

        public string WindSpeed { get; set; }

        public string Precipitation { get; set; } //Осадки

        public string DayOfWeek { get; set; }

        public string ImagePath { get; set; }

        private string GenerateRequestUri(string endpoint, Position coordinates)
        {
            var requestUri = endpoint;
            requestUri += $"?lat={coordinates.Latitude}";
            requestUri += $"&lon={coordinates.Longitude}";
            requestUri += $"&APPID={"a71121bb8192bf08110337436efdc9a2"}";         

            return requestUri;
        }

        public async Task GetWeatherByCoordinates(Position coordinates)
        {
            if (coordinates != null)
            {
                var _restService = new WeatherRestService();

                try
                {
                    var weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.currentWeatherURL, coordinates));                    

                    weatherData.Main.Temperature = Converter.KelvinToCelsius(weatherData.Main.Temperature); //Перевод из Кельвин в Цельсий

                    Temperature = Convert.ToString(weatherData.Main.Temperature) + " ℃";

                    Temperature += EngToRus(weatherData.Weather[0].Description); 
                }
                catch
                {
                    Temperature = "Температура не найдена";
                }
            }
        }

        private string EngToRus(string EngWeather)
        {
            switch (EngWeather)
            {
                case "clear sky":
                    return " Ясно";

                case "few clouds":
                    return " Облачно";
                case "overcast clouds":
                    return " Облачно";

                case "scattered clouds":
                    return " Облачно";

                case "broken clouds":
                    return " Облачно";

                case "shower rain":
                    return " Дождь";

                case "rain":
                    return " Дождь";

                case "thunderstorm":
                    return " Гром";

                case "snow":
                    return " Снегопад";

                case "mist":
                    return " Туман";
            }

            return Temperature;
        }
    }
}