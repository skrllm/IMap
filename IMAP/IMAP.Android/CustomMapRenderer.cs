using Android.Gms.Maps;
using IMAP.Droid;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(Map), typeof(CustomMapRenderer))]

namespace IMAP.Droid
{
    [System.Obsolete]
    class CustomMapRenderer : MapRenderer
    {
        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            NativeMap.SetPadding(0, 800, 0, 0);
        }
    }
}