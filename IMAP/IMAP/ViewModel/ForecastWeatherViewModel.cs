using IMAP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using IMap.Model;
using Xamarin.Forms;
using System.Windows;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Maps;

namespace IMap.ViewModel
{
    public class ForecastWeatherViewModel : ViewModel
    {

        public ObservableCollection<Grouping<string, WeatherModel>> dailyWeatherGroups { get; set; }

        public ObservableCollection<Grouping<string, WeatherModel>> DailyWeatherGroups
        {
            get => dailyWeatherGroups;
            set
            {
                dailyWeatherGroups = value;
                OnPropertyChange();
            }
        }
        public bool isIndicatorRunning { get; set; }

        public bool IsIndicatorRunning
        {
            get => isIndicatorRunning;
            set
            {
                isIndicatorRunning = value;
                OnPropertyChange();
            }

        }

        GeocoderModel geocoder = new GeocoderModel();

        

        


        private string geocoderWeatherText;
        public string GeocoderWeatherText
        {
            get => geocoderWeatherText;
            set
            {
                geocoderWeatherText = value;
                OnPropertyChange();
            }
        }

        public ICommand SearchWeatherCommand { get; set; }

        public ForecastWeatherViewModel()
        {

            SearchWeatherCommand = new Command(SearchWeatherMethod, canExecuteMethod);
            
        }

        private async void SearchWeatherMethod(object parameters)
        {
            IsIndicatorRunning = true;
            DailyWeatherGroups?.Clear();
            OnPropertyChange();

            await geocoder.SearchMethod(GeocoderWeatherText);

            if (geocoder.SearchPosition != new Position(0, 0))
            {
                GetWeather(geocoder.SearchPosition.Latitude, geocoder.SearchPosition.Longitude);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Неизвестный адрес", "Адрес не найден", "Ok");
            }

            IsIndicatorRunning = false;
        }


        private async void GetWeather(double latitude, double longtitude)
        {

            var url = Constants.ForecastWeatherURL;
            url += $"?lat={latitude}";
            url += $"&lon={longtitude}";
            url += $"&mode=xml&units=imperial";
            url += $"&APPID={Constants.OpenWeatherMapAPIKey}";



            // Create a web client.
            using (WebClient client = new WebClient())
            {
                // Get the response string from the URL.
                await DisplayForecast(client.DownloadString(url));
                try
                {
                    //  DisplayForecast(client.DownloadString(url));
                }
                catch (WebException ex)
                {
                    //DisplayError(ex);
                }
                catch (Exception ex)
                {

                    // await Application.Current.MainPage.DisplayAlert("Ошибка", "Ошибка", "Ok");
                }
            }
        }

        // Display the forecast.
        private async Task DisplayForecast(string xml)
        {
            XmlDocument xml_doc = new XmlDocument();


            xml_doc.LoadXml(xml);

            // Get the city, country, latitude, and longitude.
            XmlNode loc_node = xml_doc.SelectSingleNode("weatherdata/location");

           // DailyWeather.Clear();
           
            var Weathers = new List<WeatherModel>();

            var culture = new System.Globalization.CultureInfo("ru-RU"); //Установка языка

            foreach (XmlNode time_node in xml_doc.SelectNodes("//time"))
            {
                // Get the time in UTC.
                DateTime time = DateTime.Parse(time_node.Attributes["from"].Value, null, DateTimeStyles.AssumeUniversal);
                
                // Get the temperature.
                XmlNode temp_node = time_node.SelectSingleNode("temperature");
                string temp = temp_node.Attributes["value"].Value;



                XmlNode windSpeed_node = time_node.SelectSingleNode("windSpeed"); //Иконка погоды
                string windSpeed = windSpeed_node.Attributes["mps"].Value;

                XmlNode symbol_node = time_node.SelectSingleNode("symbol"); //Иконка погоды
                string symbol = symbol_node.Attributes["var"].Value;


                Weathers = WeathersConstructor(Weathers, time, temp,symbol,windSpeed,culture);

            }

            var groups = Weathers.GroupBy(p => p.DayOfWeek).Select(g => new Grouping<string, WeatherModel>(g.Key,g));
            // передаем группы в PhoneGroups
            DailyWeatherGroups = new ObservableCollection<Grouping<string, WeatherModel>>(groups);
            
            OnPropertyChange();
            
        }

        private List<WeatherModel> WeathersConstructor(List<WeatherModel> Weathers, DateTime time, string temp,string symbol,string windSpeed,CultureInfo culture)
        {
            string TimeOfDay ="";
            string ImagePath = "";
            if ((time.Hour >= 2) && (time.Hour <= 4) || (time.Hour >= 8) && (time.Hour <= 10) ||
                (time.Hour >= 14) && (time.Hour <= 16) || (time.Hour >= 20) && (time.Hour <= 22))
            {
                if ((time.Hour >= 2) && (time.Hour <= 4))
                {
                    TimeOfDay = "Ночь";
                    ImagePath = "Night.jpg";
                }
                if ((time.Hour >= 8) && (time.Hour <= 10))
                {
                    TimeOfDay = "Утро";
                    ImagePath = "Morning.jpg";
                }
                if ((time.Hour >= 14) && (time.Hour <= 16))
                {
                    TimeOfDay = "День";
                    ImagePath = "Day.jpg";
                }
                if ((time.Hour >= 20) && (time.Hour <= 22))
                {
                    TimeOfDay = "Вечер";
                    ImagePath = "Evening.jpg";
                }

                Weathers.Add(new WeatherModel()
                {
                    ImagePath = ImagePath,

                    Time = TimeOfDay,

                    Temperature = Convert.ToString(Converter.FahrenheitToCelsius(Convert.ToDouble(temp, CultureInfo.InvariantCulture))) + " ℃",

                    WindSpeed = Convert.ToString(Math.Round(Convert.ToDouble(windSpeed, CultureInfo.InvariantCulture) /2.237,0))+" м/с",

                    // IconURL = "http://openweathermap.org/img/wn/"+symbol+"@2x.png",

                    IconURL = "_"+symbol+".png",

                    DayOfWeek = Converter.ToUpperFirstLetter(culture.DateTimeFormat.GetDayName(time.DayOfWeek))
                });

            }



            return Weathers;
        }

    }
}
