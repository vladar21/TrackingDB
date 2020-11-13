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
        /// <summary>
        /// имя файла для хранения данных о пользователях
        /// </summary>
        static string NameUserDB = "user.db";
        /// <summary>
        /// имя файла для хранения координат
        /// </summary>
        static string NameTrackingDB = "tracking.db";
        /// <summary>
        /// наибольший ID пользователя, хранящийся в базе
        /// </summary>
        public int CurrentID { get; set; }
        /// <summary>
        /// путь к файлу, с данными о пользователях
        /// </summary>
        private string PathUserDB { get; set; }
        /// <summary>
        /// путь к файлу, с данными о координатах
        /// </summary>
        private string PathTrackingDB { get; set; }
        /// <summary>
        /// путь к логу
        /// </summary>
        private string LogPath { get; set; } = "log.txt";
        /// <summary>
        /// актуальный список пользователей
        /// </summary>
        public List<User> usersList { get; set; }
        /// <summary>
        /// актуальный список координат
        /// </summary>
        private List<Point> allPointsList { get; set; }

        public DB()
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
                var content = File.ReadAllText(PathUserDB);
                if (content != "")
                {
                    var jsonResult = JsonConvert.DeserializeObject(content).ToString();
                    var list = JsonConvert.DeserializeObject<List<User>>(jsonResult);
                    
                    usersList = list;                  
                }
                else
                {
                    usersList = new List<User>();
                }
                
            }

            PathTrackingDB = Combine(CurrentDirectory, NameTrackingDB);
            if (!File.Exists(PathTrackingDB))
            {
                StreamWriter userDB = File.CreateText(PathTrackingDB);
                userDB.Close();
                allPointsList = new List<Point>();
            }
            else
            {
                var content1 = File.ReadAllText(PathTrackingDB);
                if (content1 != "")
                {
                    var jsonResult1 = JsonConvert.DeserializeObject(content1).ToString();
                    var list1 = JsonConvert.DeserializeObject<List<Point>>(jsonResult1);

                    allPointsList = list1;
                }
                else
                {
                    allPointsList = new List<Point>();
                }

            }

        }

        /// <summary>
        /// метод поиска по имени пользователя
        /// </summary>
        public bool FindByUserName(string pattern)
        {
            var result = (from t1 in allPointsList
                          join t2 in usersList
                          on t1.UserID equals t2.ID
                          select new { t1.Latitude, t1.Longitude, t2.Name, t2.LastName, t2.Age }).ToList();
            var groupresult = from user in result group user by (user.Name, user.LastName, user.Age) into g select new { Name = g.Key, Points = from p in g select (p.Latitude, p.Longitude) };

            var findresult = groupresult.Where(r => r.Name.ToString().ToLower().Contains(pattern)).ToList();

            if (findresult.Count() > 0)
            {
                foreach (var group in findresult)
                {
                    WriteLine(group.Name);
                    foreach (var point in group.Points)
                    {
                        WriteLine(point.Latitude + " " + point.Longitude);
                    }
                }

                return true;
            }

            return false;
            
        }

        /// <summary>
        /// метод печати данных из базы
        /// </summary>
        public void PrintDB()
        {
            var result = (from t1 in allPointsList
                          join t2 in usersList
                          on t1.UserID equals t2.ID
                          select new { t1.Latitude, t1.Longitude, t2.Name, t2.LastName, t2.Age }).ToList();
            var groupresult = from user in result group user by (user.Name, user.LastName, user.Age) into g select new { Name = g.Key, Points = from p in g select (p.Latitude, p.Longitude)};

            foreach (var group in groupresult)
            {
                WriteLine(group.Name);                
                foreach (var point in group.Points)
                {
                    WriteLine(point.Latitude  + " " + point.Longitude);
                }
            }
        }

        /// <summary>
        /// метод для сохрания в базе новых координат
        /// </summary>
        public bool SavePoint(Point point)
        {
            // проверка на наличие в базе пользоватля с id, указанным при создании трек-точки
            if (usersList != null)
            {                
                var checkID = usersList.Select(u => u.ID).ToList();// == point.UserID).FirstOrDefault();
                if (checkID == null)
                {
                    // ошибка, такого ID в базе нет
                    return false;
                }
                else
                {
                    allPointsList.Add(point);
                }

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
            return false;
        }

        /// <summary>
        /// метод для сохранения в базе нового пользователя
        /// </summary>
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

        /// <summary>
        /// метод для опредения следующего по порядку идентификатора в таблице пользователей
        /// </summary>
        /// <returns></returns>
        public int GetNextUserID()
        {
            try
            {
                if (usersList != null)
                {
                    var lastID = usersList.OrderByDescending(x => x.ID).FirstOrDefault();
                    if (lastID == null) { CurrentID = 0; }
                    else { CurrentID = lastID.ID; }
                }
                else
                {
                    CurrentID = 0;
                }

                CurrentID++;
            }
            catch (Exception e)
            {
                // записать в лог-файл
                using (FileStream fs = new FileStream(LogPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("DB error: " + DateTime.Now + NewLine + e + NewLine);
                }

            }

            return CurrentID;

        }


    }


}
