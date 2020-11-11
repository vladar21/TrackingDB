using System;
using System.Collections.Generic;
using System.Text;

namespace TrackingDB
{
    public readonly struct Point
    {
        public double Latitude { get; }

        public double Longitude { get; }

        public int UserID { get; }

        public Point(double latitude, double longitude, int userid)
        {
            Latitude = latitude;
            Longitude = longitude;
            UserID = userid;
        }

    }
}
