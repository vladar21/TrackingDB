using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrackingDB
{
    public class User
    {
        [JsonProperty("ID")]
        public int ID { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("Age")]
        public string Age { get; set; }
        public List<Point> UserPoints { get; set; }

        public User(string[] newUser)
        {
            ID = 0;
            Name = newUser[0];
            LastName = newUser[1];
            Age = newUser[2];
            UserPoints = new List<Point>();
        }

    }
}
