using System.Windows.Input;
using IMap.Model;
using IMAP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace IMap.ViewModels
{
    public class MapViewModel : ViewModel
    {
        MapModel data;
        GeocoderModel geocoder;
        WeatherModel weatherModel;

        public MapViewModel()
        {
            data = new MapModel();
            geocoder = new GeocoderModel();

            ClickTypeMapCommand = new Command(ClickTypeMapMethod, canExecuteMethod);
            ClickTrafficCommand = new Command(ClickTrafficMethod, canExecuteMethod);
            SearchCommand = new Command(SearchMethod, canExecuteMethod);

            data.map.MoveToRegion(new MapSpan(new Position(0, 0), 100, 100)); //Изначальный фокус карты
           
            OnPropertyChange();
        }

        public ICommand ClickTypeMapCommand { get; set; }
        public ICommand ClickTrafficCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public Map Map 
        {
            get => data.map;
        } 

        public string GeocoderText
        {
            get => data.SearchText;
            set
            {
                if (data.SearchText == value) return;
                data.SearchText = value;
                OnPropertyChange();
            }
        }

        public string ImageTrafficButton
        {
            get => data.ImageTrafficButton;
        } 

        private void ClickTrafficMethod(object parameters)
        {
            data.map.TrafficEnabled = !data.map.TrafficEnabled;
            if (data.map.TrafficEnabled)
            {
                data.ImageTrafficButton = "EnabledTraffic.png";
            }
            else
            {
                data.ImageTrafficButton = "DisabledTraffic.png";
            }
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

        private void DrawArea()
        {
            data.map.Pins.Add(DrawPin(new Position(25,25),"Test Area","My area"));
            data.map.MapElements.Add(DrawCircle(new Position(25,25),250000));
        }

        private Circle DrawCircle(Position position, int radius)
        {
            Circle circle = new Circle
            {
                Center = position,             
                Radius = new Distance(radius),
                StrokeColor = Color.FromHex("#88FF0000"),
                StrokeWidth = 8,
                FillColor = Color.FromHex("#88FFC0CB")
            };

            return circle;
        }

        private Pin DrawPin(Position position, string label = null, string adress = null)
        {
            var pin = new Pin
            {
                Label = label,
                Address = adress,
                Type = PinType.Place,
                Position = position
            };

            return pin;
        }
    }
}