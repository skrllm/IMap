using IMAP.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IMAP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        public Master()
        {
            int i = 0;
            InitializeComponent();
            MapButton.Clicked += async (sender, e) =>
            {
                await App.NavigateMasterDetail(new Detail());

            };
            AboutButton.Clicked += async  (sender, e) =>
            {
                await App.NavigateMasterDetail(new About());

            };
            WeatherButton.Clicked += async (sender, e) =>
            {
                i++;
                if (i == 8)
                {
                    await App.NavigateMasterDetail(new EasterEgg());
                }
                else
                {
                    await App.NavigateMasterDetail(new Weather());
                }
               
            };
        }
    }
}