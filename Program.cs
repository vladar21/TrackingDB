using System;
using static System.Console;
using System.IO;
using static System.Environment;
using CommandLine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingDB
{

    class Program
    {
        /// <summary>
        /// ключ :t  признак конца ввода трекинговой информации
        /// </summary>
        public static bool T { get; set; } = true;
        /// <summary>
        /// ключ :q - конец ввода пользователей
        /// </summary>
        public static bool Q { get; set; } = true;

        private static DB db { get; set; } = new DB();

        static void Main() // string[] args
        {
            string[] args = { "add", "-k", "12345" };
            if (args.Count() > 0 && args.Count() < 4)
            {
                CLI cli = new CLI(args);

                if (cli.Add && cli.K && args.Count() == 3 && cli.Verify)
                {
                    InputNewUsers();  
                    
                }

                if (cli.Read && cli.K && args.Count() == 3 && cli.Verify)
                {
                    PrintDB();
                }

                if (cli.Find && cli.K && args.Count() == 3 && cli.Verify)
                {
                    //FindByUserName();
                }
            }
            else
            {
                WriteLine("Incorrect input datas. Try again.");
            }



            //DB db = new DB();
            //User user1 = new User("Vasily", "Pupkin", 35);
            //db.SaveUser(user1);
            //User user2 = new User("Masha", "Lopareva", 15);
            //db.SaveUser(user2);
            //Point point1 = new Point(25.222, 32.777, 1);
            //db.SavePoint(point1);
            //Point point2 = new Point(25.220, 32.780, 1);
            //db.SavePoint(point2);
            //Point point3 = new Point(25.218, 32.787, 1);
            //db.SavePoint(point3);
            //Point point4 = new Point(40.001, 60.02, 2);
            //db.SavePoint(point4);
            //Point point5 = new Point(40.003, 60.08, 2);
            //db.SavePoint(point5);

            WriteLine("The session is over");


        }

        private static void PrintDB()
        {
            //DB db = new DB();
            //foreach($item in db.usersList)
        }

        private static void InputNewUsers()
        {
            do {
                Q = true;
                string strUser = ReadLine();
                string[] readline = ParseArguments(strUser);
                if (readline[0] == ":q")
                {
                    Q = false;
                }
                else
                {
                    if (readline.Count() != 3)
                    {
                        WriteLine("Incorrect input users data. Try again. \r\b\r\b Press any keys to exit");
                        ReadKey();
                        Exit(0);
                    }

                    User user = new User(readline);
                    //DB db = new DB();
                    db.SaveUser(user);

                    do
                    {
                        T = true;
                        string strPoint = ReadLine();
                        string[] readPoint = ParseArguments(strPoint);
                        if (readPoint[0] == ":t")
                        {
                            T = false;
                        }
                        else
                        {
                            if (readPoint.Count() != 2)
                            {
                                do 
                                {
                                    T = true;
                                    WriteLine("Incorrect input users data. Try again." + Environment.NewLine + Environment.NewLine + "Or type :t to exit");
                                    strPoint = ReadLine();
                                    readPoint = ParseArguments(strPoint);
                                    if (readPoint[0] == ":t" || readPoint.Count() == 2)
                                    {
                                        T = false;
                                    }
                                } while (T);

                                T = true;
                                //ReadKey();
                                //Exit(0);
                            }
                            string ltt = string.Join("", readPoint[0]);
                            string lng = string.Join("", readPoint[1]);
                            (string latitude, string longitude, int userID) newPoint = (ltt, lng, user.ID);
                            Point point = new Point(newPoint);
                            //DB dbP = new DB();
                            db.SavePoint(point);
                        }
                    }
                    while (T);

                }

            } while (Q);
            
        }

        private static string[] ParseArguments(string commandLine)
        {
            char[] parmChars = commandLine.ToCharArray();
            bool inQuote = false;
            for (int index = 0; index < parmChars.Length; index++)
            {
                if (parmChars[index] == '"')
                    inQuote = !inQuote;
                if (!inQuote && parmChars[index] == ' ')
                    parmChars[index] = '\n';
            }
            return (new string(parmChars)).Split('\n');
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
