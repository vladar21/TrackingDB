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
                
        static void Main(string[] args)
        {
            DB db = new DB();
            User user1 = new User("Vasily", "Pupkin", 35);
            db.SaveUser(user1);
            User user2 = new User("Masha", "Lopareva", 15);
            db.SaveUser(user2);
            Point point1 = new Point(25.222, 32.777, 1);
            db.SavePoint(point1);
            Point point2 = new Point(25.220, 32.780, 1);
            db.SavePoint(point2);
            Point point3 = new Point(25.218, 32.787, 1);
            db.SavePoint(point3);
            Point point4 = new Point(40.001, 60.02, 2);
            db.SavePoint(point4);
            Point point5 = new Point(40.003, 60.08, 2);
            db.SavePoint(point5);

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
