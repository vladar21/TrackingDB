using System;
using static System.Console;
using System.IO;
using static System.Environment;
using static System.IO.Path;
using Newtonsoft.Json;


namespace TrackingDB
{  

    class Program
    {

        //readonly string jsonPathPoint = "C:/Users/Vlad/source/repos/TrackingDB/bin/Debug/netcoreapp3.1/tracking.db";

        // определение массива позывных пилота Viper
        static string[] callsigns = new string[] { "Husker", "Starbuck", "Apollo", "Boomer", "Bulldog", "Athena", "Helo", "Racetrack" };
                
        static void Main(string[] args)
        {
            string JsonPathUser = Combine(CurrentDirectory, "user.db");
            string JsonPathPoint = Combine(CurrentDirectory, "tracking.db");      

            DB db = new DB(JsonPathUser);
            User user1 = new User("Vasily", "Pupkin", 35, db);
            User user2 = new User("Masha", "Lopareva", 15, db);           
            
        }

        static void WorkWithText()
        {
            // определение файла для записи
            string textFile = Combine(CurrentDirectory, "streams.txt");
            // создание текстового файла и возвращение помощника записи
            StreamWriter text = File.CreateText(textFile);
            // перечисление строк с записью каждой из них в поток в отдельной строке
            foreach (string item in callsigns)
            {
                text.WriteLine(item);
            }
            text.Close(); // release resources
                          // вывод содержимого файла в консоль
            WriteLine($"{textFile} contains { new FileInfo(textFile).Length} bytes.");
            WriteLine(File.ReadAllText(textFile));
        }

        static void WT()
        {
            using (FileStream file2 = File.OpenWrite(Path.Combine(CurrentDirectory, "file2.txt")))
            {
                using (StreamWriter writer2 = new StreamWriter(file2))
                {
                    try
                    {
                        writer2.WriteLine("Welcome, .NET Core!");
                    }
                    catch (Exception ex)
                    {
                        WriteLine($"{ex.GetType()} says {ex.Message}");
                    }
                } // автоматически вызывает Dispose, если объект не равен null
            } // автоматически вызывает Dispose, если объект не равен null
        }
    }

    
}
