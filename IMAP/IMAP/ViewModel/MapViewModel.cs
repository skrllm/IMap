using System.Dynamic;
using System.Windows.Input;
using IMap.Model;
using IMAP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace IMap.ViewModel
{
    public class MapViewModel : ViewModel
    {
        MapModel data = new MapModel();
        GeocoderModel geocoder = new GeocoderModel();

        WeatherModel weatherModel;

        public MapViewModel()
        {
            ClickTypeMapCommand = new Command(ClickTypeMapMethod, canExecuteMethod);
            ClickTrafficCommand = new Command(ClickTrafficMethod, canExecuteMethod);

            SearchCommand = new Command(SearchMethod, canExecuteMethod);


            data.map.MoveToRegion(new MapSpan(new Position(0, 0), 100, 100)); //Изначальный фокус карты

            OnPropertyChange();
        }

        public Map Map 
        {
            get
            {
                return data.map;
            } 
        } 

        public string GeocoderText
        {
            get
            {
                return data.SearchText;
            }
            set
            {
                if (data.SearchText == value) return;
                data.SearchText = value;
                OnPropertyChange();
            }
        }

        public string ImageTrafficButton
        {
            get
            {
                return data.ImageTrafficButton;
            }
        } 

        public ICommand ClickTypeMapCommand { get; set; }
        public ICommand ClickTrafficCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        private void ClickTrafficMethod(object parameters)
        {
            data.map.TrafficEnabled = !data.map.TrafficEnabled;
            if (data.map.TrafficEnabled)
                data.ImageTrafficButton = "EnabledTraffic.png";
            else
                data.ImageTrafficButton = "DisabledTraffic.png";
            OnPropertyChange();
        }

        private async void ClickTypeMapMethod(object Parameters) //Изменение типов карты
        {
            var ActionSheet =
                await Application.Current.MainPage.DisplayActionSheet("Тип карты", "Отмена", null, "Улица", "Гибрид", "Спутник");
            switch (ActionSheet)
            {
                case "Улица":

                    data.map.MapType = MapType.Street;

                    break;

                case "Гибрид":

                    data.map.MapType = MapType.Hybrid;

                    break;

                case "Спутник":

                    data.map.MapType = MapType.Satellite;

                    break;
            }

            OnPropertyChange();
        }

        private async void SearchMethod(object Parameters)
        {
            await geocoder.SearchMethod(data.SearchText); // поиск координат по адресу

            await geocoder.ReverseGeocoder(geocoder.SearchPosition); //Поиск описания найденного адреса


            if (geocoder.SearchPosition != new Position(0, 0))
            {
                weatherModel = new WeatherModel();
                await weatherModel.GetWeatherByCoordinates(geocoder.SearchPosition);


                var pin = new Pin
                {
                    Label = geocoder.info,
                    Address = weatherModel.Temperature,
                    Type = PinType.Place,
                    Position = geocoder.SearchPosition
                };

                if (data.map.Pins.Count != 0) data.map.Pins.Clear(); //стирание старой точки

                data.map.Pins.Add(pin);

                data.map.MoveToRegion(new MapSpan(geocoder.SearchPosition, 0.2, 0.2)); //Фокус на найденную точку
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Неизвестный адрес", "Адрес не найден", "Ok");
                data.SearchText = "";
            }

            OnPropertyChange();
        }
    }
}