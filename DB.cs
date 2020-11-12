using System;
using static System.Console;
using System.IO;
using static System.Environment;
using static System.IO.Path;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;


namespace TrackingDB
{
    public class DB
    {
        protected static string NameUserDB = "user.db";
        protected static string NameTrackingDB = "tracking.db";
        public int CurrentID { get; set; }
        private string PathUserDB { get; set; }
        private string PathTrackingDB { get; set; }
        private string LogPath { get; set; } = "log.txt";
        public List<User> usersList { get; set; }
        private List<Point> userPointsList { get; set; }
        private List<Point> allPointsList { get; set; }

        public DB()
        {
            try
            {
                PathUserDB = Combine(CurrentDirectory, NameUserDB);
                if (!File.Exists(PathUserDB))
                {
                    StreamWriter userDB = File.CreateText(PathUserDB);
                    userDB.Close();
                    usersList = new List<User>();
                }
                else
                {                   
                    string JSONtxt = File.ReadAllText(PathUserDB);
                    var list = JsonConvert.DeserializeObject<List<User>>(JSONtxt);
                    if (list == null) 
                    {
                        usersList = new List<User>();
                    }
                    else
                    {
                        usersList = list;
                    }
                }

                PathTrackingDB = Combine(CurrentDirectory, NameUserDB);
                if (!File.Exists(PathTrackingDB))
                {
                    StreamWriter trackingDB = File.CreateText(PathTrackingDB);
                    trackingDB.Close();
                    allPointsList = new List<Point>();
                }
                else
                {
                    string JSONtxt = File.ReadAllText(PathTrackingDB);
                    var list = JsonConvert.DeserializeObject<List<Point>>(JSONtxt);
                    if (list == null)
                    {
                        allPointsList = new List<Point>();
                    }
                    else
                    {
                        allPointsList = list;
                    }
                }
            }
            catch(Exception e)
            {
                // записать в лог-файл
                using (FileStream fs = new FileStream(LogPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("DB error: " + DateTime.Now + "\r\b" + e + "\r\b");
                }
                
            }

        }

        public bool SavePoint(Point point)
        {
            // проверка на наличие в базе пользоватля с id, указанным при создании трек-точки
            //if (allPointsList != null)
            //{
            var IDs = from u in usersList
                             where u.ID == point.UserID
                             select new { ID = u.ID };
            var checkID = usersList.Select(u => u.ID);// == point.UserID).FirstOrDefault();

                if (checkID == null)
                {
                    // ошибка, такого ID в базе нет
                    return false;
                }
                else
                {
                    allPointsList.Add(point);
                }
            //}
            //else
            //{
            //    allPointsList = new List<Point>();
            //}

            // создание файла для записи            
            using (StreamWriter sw = File.CreateText(PathTrackingDB))
            {
                var convertedJson = JsonConvert.SerializeObject(allPointsList, Formatting.Indented);
                // создание объекта для форматирования в JSON
                var jss = new JsonSerializer();
                // сериализация графа объектов в строку
                jss.Serialize(sw, convertedJson);
            }

            return true;
        }

        public bool SaveUser(User user)
        {
            GetNextUserID();
            user.ID = CurrentID;
                      
            if (usersList == null)
            {
                usersList = new List<User>();
            }
            usersList.Add(user);

            // создание файла для записи            
            using (StreamWriter sw = File.CreateText(PathUserDB))
            {
                var convertedJson = JsonConvert.SerializeObject(usersList, Formatting.Indented);
                // создание объекта для форматирования в JSON
                var jss = new JsonSerializer();
                // сериализация графа объектов в строку
                jss.Serialize(sw, convertedJson);
            }
            return true;
        }

        public int GetNextUserID()
        {
            try
            {
                if (usersList != null)
                {
                    var lastID = usersList.OrderByDescending(x => x.ID).FirstOrDefault();
                    if (lastID == null) { CurrentID = 0;  }
                    else { CurrentID = lastID.ID;  }
                }
                else
                {
                    CurrentID = 0;
                }
            
                CurrentID++;
            }
            catch(Exception e)
            {
                // записать в лог-файл
                using (FileStream fs = new FileStream(LogPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("DB error: " + DateTime.Now + "\r\b" + e + "\r\b");
                }
                
            }

            return CurrentID;
            
        }


    }

    
}
