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
        public int Age { get; set; }
        private List<Point> UserPoints { get; set; }

        public User(string name, string lastName, int age)
        {
            ID = 0;
            Name = name;
            LastName = lastName;
            Age = age;
            UserPoints = new List<Point>();
        }

    }
}
