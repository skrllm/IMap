using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMAP.Model;
using IMap.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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