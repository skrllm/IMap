using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IMAP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EasterEgg : ContentPage
    {
        public EasterEgg()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}