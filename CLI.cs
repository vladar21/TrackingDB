using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TrackingDB
{
    /// <summary>
    /// класс для работы с командной строкой
    /// </summary>
    public class CLI
    {
        public bool Add { get; set; }
        public bool Read { get; set; }
        public bool Find { get; set; }
        public bool K { get; set; }
        private readonly string password = "12345";
        public bool Verify { get; set; }

        public CLI (string[] args)
        {
            IEnumerable<string> list = args.ToList();
            Add = list.Contains("add");
            Read = list.Contains("read");
            Find = list.Contains("find");
            K = list.Contains("-k");
            if (args.Count() == 4)
            {
                Verify = args[3] == password ? true : false;
            }
            else
            {
                Verify = args[2] == password ? true : false;
            }
            
        }
    }
}
