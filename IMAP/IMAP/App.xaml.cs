using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IMAP.Views;
using System.Threading.Tasks;

namespace IMAP
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetail { get; set; }
        public async static Task NavigateMasterDetail(Page page)
        {
            App.MasterDetail.IsPresented = false;
           await App.MasterDetail.Detail.Navigation.PushAsync(page);
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
