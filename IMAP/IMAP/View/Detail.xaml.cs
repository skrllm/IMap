using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace IMAP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detail : ContentPage
    {
        public Detail()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
        }
    }
}