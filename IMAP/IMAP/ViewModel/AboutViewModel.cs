using System.Windows.Input;
using Xamarin.Essentials;

namespace IMap.ViewModel
{
    public class AboutViewModel : ViewModel
    {
        public ICommand SeeMoreButtonCommand { get; set; }

        public AboutViewModel()
        {
            SeeMoreButtonCommand = new Command(SeeMoreButtonMethod, canExecuteMethod);

            OnPropertyChange();

        }
        private async void SeeMoreButtonMethod(object parameters)
        {
            await Browser.OpenAsync("https://github.com/skrllm", BrowserLaunchMode.SystemPreferred);
        }
    }
}


