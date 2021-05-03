using System.ComponentModel;
using Xamarin.Forms;

namespace IMAP.Views
{
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