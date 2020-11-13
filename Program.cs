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
        /// <summary>
        /// контекст базы данных
        /// </summary>
        private static DB db { get; set; } // = new DB();

        static void Main() // string[] args
        {
            db = new DB();

            //string[] args = { "add", "-k", "12345" };
            //string[] args = { "read", "-k", "12345" };
            string[] args = { "find", "-k", "12345" };

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
                    FindByUserName("e");
                }
            }
            else
            {
                WriteLine("Incorrect input datas. Try again.");
            }

            WriteLine("The session is over");
        }

        private static void FindByUserName(string pattern)
        {
            pattern = pattern.ToLower();
            db.FindByUserName(pattern);
        }
        private static void PrintDB()
        {
            db.PrintDB();
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
                        WriteLine("Incorrect input users data. Try again." + NewLine + NewLine + "Press any keys to exit");
                        ReadKey();
                        Exit(0);
                    }

                    User user = new User(readline);
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
                                    WriteLine("Incorrect input users data. Try again." + NewLine + NewLine + "Or type :t to exit");
                                    strPoint = ReadLine();
                                    readPoint = ParseArguments(strPoint);
                                    if (readPoint[0] == ":t" || readPoint.Count() == 2)
                                    {
                                        T = false;
                                    }
                                } while (T);

                                T = true;

                            }
                            string ltt = string.Join("", readPoint[0]);
                            string lng = string.Join("", readPoint[1]);
                            (string latitude, string longitude, int userID) newPoint = (ltt, lng, user.ID);
                            Point point = new Point(newPoint);

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
