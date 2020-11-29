using System.ComponentModel;
using Xamarin.Forms;

namespace IMAP.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.Master = new Master();
            this.Detail = new NavigationPage(new Detail());
            App.MasterDetail = this;
        }


    }
}