
using System;
using Newtonsoft.Json;

namespace TrackingDB
{
    public class Point
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int UserID { get; set; }

        public Point() { }
        public Point((string latitude, string longitude, int userID) newPoint)
        {
            Latitude = newPoint.latitude;
            Longitude = newPoint.longitude;
            UserID = newPoint.userID;
        }

        public override string ToString()
        {
            return Latitude + " " + Longitude;
        }
    }
}
