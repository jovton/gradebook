using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook
{
    public class Book
    {
        private List<double> grades;

        public string Name { get; set; }

        public Book(string name)
        {
            Name = name;
            grades = new List<double>();
        }

        public void AddGrade(double grade)
        {
            grades.Add(grade);
        }

        public Statistics  ComputeStatistics()
        {
            var stats = new Statistics();

            stats.High = grades.Any() ? HighGrade() : double.MaxValue;
            stats.Low = grades.Any() ? LowGrade() : double.MinValue;
            stats.Average = grades.Any() ? Average() : 0;

            return stats;
        }

        public double Average()
        {
            return grades.Average();
        }

        public double HighGrade()
        {
            return grades.Max();
        }

        public double LowGrade()
        {
            return grades.Min();
        }

        public void ShowStatistics()
        {
            var stats = ComputeStatistics();

            var numbersString = string.Join(" and ", grades.Select(n => $"{n}"));

            var avgMessage = $"The average grade of {numbersString} is {stats.Average:N1}.";
            Console.WriteLine(avgMessage);

            var lowMessage = $"The lowest grade of {numbersString} is {stats.Low:N1}.";
            Console.WriteLine(lowMessage);

            var highMessage = $"The highest grade of {numbersString} is {stats.High:N1}.";
            Console.WriteLine(highMessage);
        }
    }
}
