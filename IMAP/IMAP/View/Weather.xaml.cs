using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IMAP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Weather : ContentPage
    {
        public Weather()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);           
        }
    }
}