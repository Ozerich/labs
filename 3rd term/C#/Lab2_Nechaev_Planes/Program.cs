using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2_Nechaev_Planes
{
    class Program
    {
        static void Main(string[] args)
        {
			Application app = new Application();
			
			app.AddPlane(new Plane("plane1.txt"));
            app.AddPlane(new Plane("plane2.txt"));

            app.Start();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.Escape)
                    Environment.Exit(0);

                app.HandleKey(key);
            }
        }
    }
}
