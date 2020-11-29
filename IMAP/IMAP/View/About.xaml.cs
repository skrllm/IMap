using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IMAP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About : ContentPage
    {
        public About()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}