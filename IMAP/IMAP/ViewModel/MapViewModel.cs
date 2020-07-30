using IMap.Model;
using System.Windows.Input;
using Xamarin.Forms.Maps;

namespace IMap.ViewModel
{
    public class MapViewModel : ViewModel
    {

        MapModel data = new MapModel();

        
        public MapType MapType //Биндинг искомого элемента
        {
            get { return data.map.MapType; }

        }

        public ICommand ChangeTypeSatteliteCommand { get; set; }
        public ICommand ChangeTypeStreetCommand { get; set; }
        public ICommand ChangeTypeHybridCommand { get; set; }

        public MapViewModel()
        {
            ChangeTypeSatteliteCommand = new Command(ChangeTypeSatteliteMethod, canExecuteMethod);
            ChangeTypeStreetCommand = new Command(ChangeTypeStreetMethod, canExecuteMethod);
            ChangeTypeHybridCommand = new Command(ChangeTypeHybridMethod, canExecuteMethod);
            data.map = new Map();

            
            OnPropertyChange();

        }
        private void ChangeTypeSatteliteMethod(object Parameters)
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


    }
}

