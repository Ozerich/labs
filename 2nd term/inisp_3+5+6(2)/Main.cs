using System;

namespace laba3
{
    class MainClass
    {
        public static int Main()
        {
            string cmd;
            int val;
            Queue<int> q = new Queue<int>();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Commands: push, pop, front, length, print, exit");
                Console.Write("? ");
                cmd = Console.ReadLine();
                try
                {
                    if (cmd == "push")
                    {
                        Console.Write("Value: ");
                        val = Int32.Parse(Console.ReadLine());
                        q.Push(val);
                    }
                    else if (cmd == "pop")
                    {
                        q.Pop();
                        Console.WriteLine("first element is deleted");
                    }
                    else if (cmd == "front")
                        Console.WriteLine(q.Front());
                    else if (cmd == "length")
                        Console.WriteLine(q.Length);
                    else if (cmd == "exit")
                        break;
                    else if (cmd == "print")
                        q.Print();
                    else
                        Console.WriteLine("Incorrect command");
                }
                catch (EmptyQueue)
                {
                    Console.WriteLine("Empty queue");
                }
                Console.WriteLine();                
            }
            return 0;
        }
    }
}