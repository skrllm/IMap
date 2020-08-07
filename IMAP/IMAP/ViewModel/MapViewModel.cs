using IMap.Model;
using System;
using System.Windows.Input;
using Xamarin.Forms.Maps;
using System.Reflection;

namespace IMap.ViewModel
{
    public class MapViewModel : ViewModel
    {

        MapModel data = new MapModel();
        GeocoderModel geocoder = new GeocoderModel();
        public Map Map
        {
            get { return data.map; }

        }
        public string GeocoderText
        {
            get { return data.SearchText; }
            set
            {
                if (data.SearchText == value) return;
                data.SearchText = value;
                OnPropertyChange();
            }
        }


        public ICommand ChangeTypeSatteliteCommand { get; set; }
        public ICommand ChangeTypeStreetCommand { get; set; }
        public ICommand ChangeTypeHybridCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public MapViewModel()
        {
            ChangeTypeSatteliteCommand = new Command(ChangeTypeSatteliteMethod, canExecuteMethod);
            ChangeTypeStreetCommand = new Command(ChangeTypeStreetMethod, canExecuteMethod);
            ChangeTypeHybridCommand = new Command(ChangeTypeHybridMethod, canExecuteMethod);
            SearchCommand = new Command(SearchMethod, canExecuteMethod);

            data.map = new Map();
            data.map.MoveToRegion(new MapSpan(new Position(0,0), 100, 100)); //Изначальный фокус карты

            OnPropertyChange();

        }
        private void ChangeTypeSatteliteMethod(object Parameters) //Изменение типов карты
        {
            data.map.MapType = MapType.Satellite;
            OnPropertyChange();
        }
        private void ChangeTypeStreetMethod(object Parameters)
        {
            data.map.MapType = MapType.Street;
            OnPropertyChange();
        }
        private void ChangeTypeHybridMethod(object Parameters)
        {
            data.map.MapType = MapType.Hybrid;
            OnPropertyChange();
        }
        async private void SearchMethod(object Parameters)
        {
            await geocoder.SearchMethod(data.SearchText); // поиск координат по адресу
            await geocoder.ReverseGeocoder(geocoder.position); //Поиск описания найденного адреса
            string PositionName = geocoder.info;

            Pin pin = new Pin
            {
                Label = PositionName,
               // Address = a,
                Type = PinType.Place,
                Position = geocoder.SearchPosition
                
            };

            if (data.map.Pins.Count != 0) //стирание старой точки
            {
                data.map.Pins.Clear();
            }
            data.map.Pins.Add(pin);

            data.map.MoveToRegion(new MapSpan(geocoder.SearchPosition, 0.2, 0.2)); //Фокус на найденную точку
            OnPropertyChange();
        }



    }
}

