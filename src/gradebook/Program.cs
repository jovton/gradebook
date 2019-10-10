using System;
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

            var numbers = new [] { 12.7, 4, 77.41 };
            var sum = numbers.Sum();
            var computationString = string.Join(" + ", numbers.Select(n => $"{n}"));
            var message = $"{computationString} = {sum}.";
            
            Console.WriteLine(message);
        }
    }
}
