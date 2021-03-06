﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace Lab5_Ozierski_PluginApplication
{
    public class Program
    {
        static List<IPlugin> plugins;

        static void FindPlugins()
        {
            plugins = new List<IPlugin>(10);

            string folder = System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["PluginDirectory"] + "\\";

            string[] files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);

            foreach (string filename in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(filename);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("IPlugin");
                        if (iface != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading plugin: " + ex.Message);
                }
            }
        }

        static void Main(string[] args)
        {
            FindPlugins();
            LongNumber num_a = new LongNumber(0);
            LongNumber num_b = new LongNumber(0);
            int user_choise = 0;
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n1 - Input number A");
                Console.WriteLine("2 - Input number B");
                Console.WriteLine("3 - Do operation");
                Console.WriteLine("4 - Exit Application");
                Console.Write("? ");

                try
                {
                    user_choise = Int32.Parse(Console.ReadLine());
                    switch (user_choise)
                    {
                        case 1:
                            Console.Write("Number: ");
                            num_a = new LongNumber(Console.ReadLine());
                            break;
                        case 2:
                            Console.Write("Number: ");
                            num_b = new LongNumber(Console.ReadLine()); ;
                            break;
                        case 3:
                            int i = 1;
                            try
                            {
                                foreach (IPlugin plugin in plugins)
                                {
                                    var pluginName = Attribute.GetCustomAttribute(plugin.GetType(), typeof(PluginNameAttribute));
                                    PluginNameAttribute atr = (PluginNameAttribute)pluginName;
                                    Console.WriteLine(i++ + "- " + atr.Name);
                                }
                                Console.Write("Your action: ");
                                int op = Int32.Parse(Console.ReadLine());
                                IPlugin plug = plugins[op - 1];
                                LongNumber res = plug.DoOp(num_a, num_b);
                                Console.WriteLine("\nResult: " + res);

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Incorrect operation");
                            }
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input error! Try Again");
                }
            }
        }

    }
}
