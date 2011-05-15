using System;

class MainEntryPoint
{
    public static int Main()
    {
        Console.WriteLine("Lab №4, Ozierski Vital, group 052004");

        IntHashTable ih = new IntHashTable(50);
        StringHashTable sh = new StringHashTable(16);

        string cmd, command, mode;
        bool found;
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Commands: add, find, print");
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
                    if (command == "add")
                    {
                        Console.Write("Value: ");
                        cmd = Console.ReadLine();
                        if (mode == "string")
                            sh.Add(cmd);
                        else if (mode == "int")
                            ih.Add(Int32.Parse(cmd));
                    }
                    else if (command == "find")
                    {
                        Console.Write("Value: ");
                        cmd = Console.ReadLine();
                        found = false;
                        if (mode == "string")
                            found = sh.Find(cmd);
                        else if (mode == "int")
                            found = ih.Find(Int32.Parse(cmd));
                        Console.WriteLine(found ? "Found" : "No found");
                    }
                    else if (command == "print")
                    {
                        if (mode == "string")
                            sh.Print();
                        else if (mode == "int")
                            ih.Print();
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