using System;

namespace Lab2
{
    class MainClass
    {
        private static double Calculate(double x, double y, double z)
        {
            double result = Math.Pow(2, -x);
            result *= Math.Sqrt(x + Math.Pow(Math.Abs(y), 1 / 4));
			try{
				result *= Math.Pow(Math.Pow(Math.E, (x - 1) / Math.Sin(z)), 1 / 3);
		}catch(DivideByZeroException e)
			{
				Console.WriteLine("By zero");
			}
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