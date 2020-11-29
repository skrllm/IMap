using Android.Gms.Maps;
using IMAP.Droid;
using IMAP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace IMAP.Droid

{
    class CustomMapRenderer:MapRenderer
    {
        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            NativeMap.SetPadding(0, 800, 0, 0);
        }
    }
}