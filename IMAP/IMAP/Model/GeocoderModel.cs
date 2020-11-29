using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace IMap.Model
{
    public class GeocoderModel
    {

        public Geocoder geocoder;
        public string info;
        public Position SearchPosition;

        public GeocoderModel()
        {
            geocoder = new Geocoder();
        }

        public async Task SearchMethod(string SearchText) //Поиск координат по адресу
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(SearchText)) //не пуст ли запрос
                {
                    var approximateLocations = await geocoder.GetPositionsForAddressAsync(SearchText);

                    SearchPosition = approximateLocations.FirstOrDefault();

                    SearchPosition = new Position(SearchPosition.Latitude, SearchPosition.Longitude);
                }
            }
            catch { }
        }

        public async Task ReverseGeocoder(Position position) //поиск информации по координатам
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    info = $"{placemark.Locality}, " + //город
                           $"{placemark.Thoroughfare}, " + //Улица
                           $"{placemark.FeatureName}"; //дом
                }
            }
            catch { }
        }
    }
}