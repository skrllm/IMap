using Xamarin.Forms.Maps;

namespace IMap.Model
{
    class MapModel
    {
        public Map map { get; set; }

        public string SearchText { get; set; }

        public string ImageTrafficButton { get; set; }

        public MapModel()
        {
            this.map = new Map {IsShowingUser = true};

            if (map.TrafficEnabled == true)
            {
                ImageTrafficButton = "EnabledTraffic.png";
            }
            else
            {
                ImageTrafficButton = "DisabledTraffic.png";
            }
        }
    }
}
