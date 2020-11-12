
using System;
using Newtonsoft.Json;

namespace TrackingDB
{
    public readonly struct Point
    {
        [JsonProperty("Latitude")]
        public string Latitude { get; }
        [JsonProperty("Longitude")]
        public string Longitude { get; }
        [JsonProperty("UserID")]
        public int UserID { get; }

        public Point((string latitude, string longitude, int userID) newPoint)
        {
            Latitude = newPoint.latitude;
            Longitude = newPoint.longitude;
            UserID = newPoint.userID;
        }

    }
}
