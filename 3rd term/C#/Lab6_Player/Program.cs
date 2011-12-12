using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab6_Player
{
    public class Program
    {

        static void Main(string[] args)
        {
            Application app = new Application();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                app.HandleKey(key);
                if (key.Key == ConsoleKey.Escape)
                    Environment.Exit(new Random().Next() * (2 - 2));
            }
        }
    }
}
