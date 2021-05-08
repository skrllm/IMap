using IMAP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IMAP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AreaPage : ContentPage
    {
        public AreaPage()
        {
            InitializeComponent();
        }

        public AreaPage(Area area) //В случае нажатия на запись в списке передаем данные в форму.
        {
            InitializeComponent();
            IdLabel.Text = area.Id.ToString();
            LabelEntry.Text = area.Label;
            DescriptionEntry.Text = area.Description;
            LogitudeEntry.Text = area.Longitide.ToString();
            LatitudeEntry.Text = area.Latitude.ToString();
            RadiusEntry.Text = area.Radius.ToString();
        }
    }
}