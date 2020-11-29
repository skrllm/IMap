using System.ComponentModel;

namespace IMap.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool canExecuteMethod(object parameter)
        {
            return true;
        }

        protected void OnPropertyChange(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
