using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackingDB
{
    public class User
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }

        public User(string name, string lastName, int age, DB db)
        {
            ID = db.GetNextUserID();
            Name = name;
            LastName = lastName;
            Age = age;
            db.Save(this);
        }

    }
}
