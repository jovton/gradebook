using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("jovton's Grade Book.");

            var grades = new List<double>() { 12.7, 4, 77.41, 5.123 };

            foreach (var grade in grades)
            {
                book.AddGrade(grade);
            }

            var stats = book.ComputeStatistics();

            ShowGradeStats(grades, stats);
        }

        private static void ShowGradeStats(List<double> grades, Statistics stats)
        {
            var numbersString = string.Join(" and ", grades.Select(n => $"{n}"));

            var avgMessage = $"The average grade of {numbersString} is {stats.Average:N1}.";
            Console.WriteLine(avgMessage);

            var letterMessage = $"The average letter grade of that is {stats.Letter}.";
            Console.WriteLine(letterMessage);

            var lowMessage = $"The lowest grade of {numbersString} is {stats.Low:N1}.";
            Console.WriteLine(lowMessage);

            var highMessage = $"The highest grade of {numbersString} is {stats.High:N1}.";
            Console.WriteLine(highMessage);
        }
    }
}
