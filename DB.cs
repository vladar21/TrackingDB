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


        private string Path { get; set; }
        private string LogPath { get; set; } = "log.txt";

        public DB(string path)
        {
            Path = path;
        }

        public void Save(object obj)
        {
            // создание файла для записи            
            StreamWriter jsonStream = File.CreateText(Path);

            // создание объекта для форматирования в JSON
            var jss = new JsonSerializer();

            // сериализация графа объектов в строку
            jss.Serialize(jsonStream, obj);
            jsonStream.Close(); // разблокировать файл
            WriteLine();
            WriteLine($"Written {new FileInfo(Path).Length} bytes of JSON to: { Path } ");

            // отображение сериализованного графа объектов
            WriteLine(File.ReadAllText(Path));
        }

        public int GetNextUserID()
        {
            try
            {
                //string jsonString = File.ReadAllText(JsonPathUser);
                //string jsonString1 = jsonString;
                //List<User> userList= JsonConvert.DeserializeObject<List<User>>(jsonString1).ToList();            
              

                if (File.Exists(Path))
                {
                    string JSONtxt = File.ReadAllText(Path);
                    var usersList = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(JSONtxt);

                    //int currentID = usersList.OrderByDescending(x => x.ID).First().ID;
                    //var entries = JSON.Deserialize<Dictionary<string, DbTableEntryModel>>(jsonString).Select(x => x.Value).ToList();
                    //currentID++;
                    return 1;

                }
                return 0;
                
            }
            catch(Exception e){
                // записать в лог-файл
                using (FileStream fs = new FileStream(LogPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("DB error: " + DateTime.Now + "\r\b" + e + "\r\b");
                }
                
                return 0;
            }
            
        }



    }

    
}
