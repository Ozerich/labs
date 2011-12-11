using System;

class MainEntryPoint
{
    public static int Main()
    {
        Console.WriteLine("Lab №4, Ozierski Vital, group 052004");
        
        StringList sl = new StringList();
        IntList il = new IntList();

        string cmd, command, mode;
        bool found;
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Commands: addback, addfront, getfront, getback, find, print");
            Console.WriteLine("Format: <command> <int|string> or exit");
            Console.Write("? ");
            cmd = Console.ReadLine();
            if (cmd == "exit")
                break;
            if (cmd.Split(' ').Length != 2)
                Console.WriteLine("Incorrect command format");
            else
            {
                command = cmd.Split(' ')[0];
                mode = cmd.Split(' ')[1];
                if (mode != "string" && mode != "int")
                    Console.WriteLine("Incorrect mode");
                else
                {
                    if (command == "addback")
                    {
                        Console.Write("Value: ");
                        if (mode == "int")
                            il.AddBack(Int32.Parse(Console.ReadLine()));
                        else if (mode == "string")
                            sl.AddBack(Console.ReadLine());
                    }
                    else if (command == "addfront")
                    {
                        Console.Write("Value: ");
                        if (mode == "int")
                            il.AddFront(Int32.Parse(Console.ReadLine()));
                        else if (mode == "string")
                            sl.AddFront(Console.ReadLine());
                    }
                    else if (command == "print")
                    {
                        if (mode == "int")
                            il.Print();
                        else if (mode == "string")
                            sl.Print();
                    }
                    else if (command == "getfront")
                    {
                        if (mode == "int")
                            Console.WriteLine(il.GetFront());
                        else if (mode == "string")
                            Console.WriteLine(sl.GetFront());
                    }
                    else if (command == "getback")
                    {
                        if (mode == "int")
                            Console.WriteLine(il.GetBack());
                        else if (mode == "string")
                            Console.WriteLine(sl.GetBack());
                    }
                    else if (command == "find")
                    {
                        Console.Write("Value: ");
                        found = false;
                        if (mode == "int")
                            found = il.Find(Int32.Parse(Console.ReadLine()));
                        else if (mode == "string")
                            found = sl.Find(Console.ReadLine());
                        Console.WriteLine(found ? "found" : "no found");
                    }
                    else 
                        Console.WriteLine("Incorrect Command");
                }
                Console.WriteLine();
            }
        }

        return 0;
    }
}