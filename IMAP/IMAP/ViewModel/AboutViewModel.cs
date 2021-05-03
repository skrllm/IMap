using System.Windows.Input;
using Xamarin.Essentials;

namespace IMap.ViewModel
{
    public class AboutViewModel : ViewModel
    {
        public AboutViewModel()
        {
            SeeMoreButtonCommand = new Command(SeeMoreButtonMethod, canExecuteMethod);
            OnPropertyChange();
        }

        public ICommand SeeMoreButtonCommand { get; set; }

        private async void SeeMoreButtonMethod(object parameters)
        {
            await Browser.OpenAsync("https://github.com/skrllm", BrowserLaunchMode.SystemPreferred);
        }
    }
}