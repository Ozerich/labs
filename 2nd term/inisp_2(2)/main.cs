using System;

namespace Lab2
{
    class MainClass
    {
        private static double Calculate(double x, double y, double z)
        {
            double result;
            result = Math.Pow(8 + Math.Sqrt(Math.Abs(x - y)), 1/3);
            result /= (x * x + y * y + 2);
            result -= Math.Pow(Math.E, Math.Abs(x - y)) * (Math.Tan(z) + 1);

			return result;
        }

        public static void Main(string[] argv)
        {
            while (true)
            {
                Console.WriteLine("Enter x, y, z values or 'exit' to quit");
                string cmd = Console.ReadLine();
                if (cmd == "exit")
                    break;
                
                string[] args = cmd.Split(' ');
                if (args.Length != 3)
                    Console.WriteLine("Incorrect params count");
                else
                {
                    double x, y, z, result;
                    
                    x = Double.Parse(args[0]);
                    y = Double.Parse(args[1]);
                    z = Double.Parse(args[2]);
                    result = Calculate(x, y, z);
                    Console.WriteLine("a = {0}", result);
                }
                Console.WriteLine("\n");
            }
        }
    }
}