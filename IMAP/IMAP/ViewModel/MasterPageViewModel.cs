using IMAP;
using IMAP.View;
using IMAP.Views;
using System.Windows.Input;

namespace IMap.ViewModel
{
    public class MasterPageViewModel : ViewModel
    {
        private int easterEggCounter = 0;
        public MasterPageViewModel()
        {
            ClickMapDetailCommand = new Command(ClickMapDetailMethod, canExecuteMethod);
            ClickWheatherCommand = new Command(ClickWheatherMethod, canExecuteMethod);
            ClickAboutCommand = new Command(ClickAboutMethod, canExecuteMethod);
        }

        public ICommand ClickMapDetailCommand { get; set; }
        public ICommand ClickWheatherCommand { get; set; }
        public ICommand ClickAboutCommand { get; set; }

        private async void ClickMapDetailMethod(object parameters)
        {
            await App.NavigateMasterDetail(new Detail());
        }

        private async void ClickWheatherMethod(object Parameters)
        {
            easterEggCounter++;
            if (easterEggCounter == 8)
            {
                await App.NavigateMasterDetail(new EasterEgg());
            }
            else
            {
                await App.NavigateMasterDetail(new Weather());
            }
        }

        private async void ClickAboutMethod(object Parameters)
        {
            await App.NavigateMasterDetail(new About());
        }
    }
}