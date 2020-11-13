using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrackingDB
{
    public class User
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; } 
        public string Age { get; set; }

        [JsonIgnore]
        public List<Point> UserPoints { get; set; }
        public User() { }
        public User(string[] newUser)
        {
            ID = 0;
            Name = newUser[0];
            LastName = newUser[1];
            Age = newUser[2];
            //UserPoints = new List<Point>();
        }

        public User(int id, string name, string lastname, string age, List<Point> userpoints)
        {
            ID = id;
            Name = name;
            LastName = lastname;
            Age = age;
            //UserPoints = userpoints;
        }

        public override string ToString() 
        {
            return Name + " " + LastName + " " + Age;
        }
    }
}
