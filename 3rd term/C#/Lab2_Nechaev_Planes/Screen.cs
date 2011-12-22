using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab2_Nechaev_Planes
{
    public static class Screen
    {
        const int WIDTH = 120;
        const int HEIGHT = 20;
        private static StringBuilder[] buffer = new StringBuilder[HEIGHT];
        public static Thread Painter { get; private set; }
        private static object obj = new Object();

        private static void Paint()
        {
            while (true)
            {
                lock (obj)
                    Flush();
                Thread.Sleep(50);
            }
        }

        static Screen()
        {
            Console.SetWindowSize(WIDTH, HEIGHT + 1);
            Console.SetBufferSize(WIDTH, HEIGHT + 1);
            Console.CursorVisible = false;

            for (int i = 0; i < HEIGHT; i++)
            {
                buffer[i] = new StringBuilder(WIDTH);
                for(int j = 0; j < WIDTH; j++)
                    buffer[i].Append(" ");
            }

            Painter = new Thread(new ThreadStart(Paint));
            Painter.Start();
        }

        public static void Flush()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(buffer[i]);
            }
            Console.SetCursorPosition(0, 0);
        }

        public static void Write(int x, int y, string s)
        {
            for (int i = 0; i < s.Length; i++)
                buffer[y][x + i] = s[i];
        }

    }
}
