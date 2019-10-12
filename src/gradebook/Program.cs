using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook book = new InMemoryBook("jovton's Grade Book");
            book.GradeAdded += OnGradeAdded;

            Console.WriteLine("");
            Console.WriteLine($"{book.Name} version 0.0.1:");
            Console.WriteLine("");
            
            EnterGrades(args, book);

            if (book.HasGrades)
            {
                var stats = book.ComputeStatistics();
                ShowGradeStats(stats);
            }
        }

        private static void EnterGrades(string[] args, IBook book)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter grades. Enter \"Q\" to start computation.");
                Console.WriteLine("");

                var input = string.Empty;

                while (input.ToUpper() != "Q")
                {
                    var pronoun = book.HasGrades ? "Next" : "First";
                    Console.Write($"{pronoun} grade (Enter 'Q' to stop): ");
                    input = Console.ReadLine();

                    if (input.ToUpper() != "Q")
                        try
                        {
                            AddGrade(book, input);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                }
            }
            else
            {
                foreach (var arg in args)
                {
                    try
                    {
                        AddGrade(book, arg);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                        break;
                    }
                }
            }
        }

        private static void OnGradeAdded(object source, EventArgs args)
        {
            Console.WriteLine("Grade added.");
        }

        private static void AddGrade(IBook book, string input)
        {
            if (double.TryParse(input, out double grade))
            {
                book.AddGrade(grade);
            }
            else if (input.Length == 1)
            {
                book.AddGrade(input[0]);
            }
            else
            {
                throw new ArgumentException($"ERROR: Invalid grade '{input}'. Enter a value between 0 and 100, or a letter from A to D, or F.");
            }
        }

        private static void ShowGradeStats(Statistics stats)
        {
            Console.WriteLine();
            Console.WriteLine($"The highest grade is {stats.High:N1}.");
            Console.WriteLine($"The lowest grade is {stats.Low:N1}.");
            Console.WriteLine($"The average grade is {stats.Average:N1}.");
            Console.WriteLine($"The average letter grade is {stats.Letter}.");
            Console.WriteLine();
        }
    }
}
