
using Newtonsoft.Json;

namespace TrackingDB
{
    public readonly struct Point
    {
        [JsonProperty("Latitude")]
        public double Latitude { get; }
        [JsonProperty("Longitude")]
        public double Longitude { get; }
        [JsonProperty("UserID")]
        public int UserID { get; }

        public Point(double latitude, double longitude, int userid)
        {
            Latitude = latitude;
            Longitude = longitude;
            UserID = userid;
        }

    }
}
