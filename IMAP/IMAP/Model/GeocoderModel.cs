using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Net;
using System.Collections.Generic;
using System.Globalization;
using System.Dynamic;
using Xamarin.Forms.Maps;
using IMap.Model;
using System.Windows.Input;
using Xamarin.Forms.Xaml.Internals;
using System.Linq;
using Xamarin.Essentials;
using IMAP;
using System.Threading;
using Stateless.Reflection;
using System.Threading.Tasks;

namespace IMap.Model
{
    public class GeocoderModel
    {
        Geocoder geocoder;

        public Position SearchPosition;
        public Position position;
        public string info;
        public GeocoderModel()
        { 
            geocoder = new Geocoder();
        } 

         async public Task SearchMethod(string SearchText) //Поиск координат по адресу
         {
            if (!string.IsNullOrWhiteSpace(SearchText)) //не пуст ли запрос
            {
                IEnumerable<Position> approximateLocations = await geocoder.GetPositionsForAddressAsync(SearchText);

                position = approximateLocations.FirstOrDefault();

                this.SearchPosition = new Position(position.Latitude, position.Longitude);

            }
         }
        async public Task ReverseGeocoder(Position position) //поиск информации по координатам
        {

            var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude,position.Longitude);

            var placemark = placemarks?.FirstOrDefault();
            if (placemark != null) 
            {
                info =  $"{placemark.Locality}, " +     //город
                        $"{placemark.Thoroughfare}, "+      //Улица
                        $"{placemark.FeatureName}";     //дом

            }

        }

    }

}
