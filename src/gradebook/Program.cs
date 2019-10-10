using System;

namespace gradebook
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine($"Hello, {args[0]}!");
            }
            else
            {
                Console.WriteLine($"Hello!");
            }

            double x = 34.1;
            double y = 15.9;

            var z = x + y;
            Console.WriteLine($"{x} + {y} is {z}.");
        }
    }
}
