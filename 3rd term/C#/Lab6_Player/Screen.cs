using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab6_Player
{
    public static class Screen
    {
        private static StringBuilder[] buffer = new StringBuilder[50];
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
            Console.SetWindowSize(80, 51);
            Console.SetBufferSize(80, 51);
            Console.CursorVisible = false;

            for (int i = 0; i < 50; i++)
            {
                buffer[i] = new StringBuilder(80);
                for(int j = 0; j < 80; j++)
                    buffer[i].Append(" ");
            }

            Painter = new Thread(new ThreadStart(Paint));
            Painter.Start();
        }

        public static void Flush()
        {
            for (int i = 0; i < 50; i++)
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
