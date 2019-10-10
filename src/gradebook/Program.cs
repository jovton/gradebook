using System;
using System.Collections.Generic;
using System.Linq;

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
                Console.WriteLine("Hello!");
            }

            var grades = new List<double>() { 12.7, 4, 77.41 };
            var avg = grades.Average();
            var computationString = string.Join(" and ", grades.Select(n => $"{n}"));
            var message = $"The average of {computationString} is {avg}.";
            
            Console.WriteLine(message);
        }
    }
}
