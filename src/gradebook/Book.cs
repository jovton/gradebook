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
            if (grade >= 0 && grade <= 100)
            {
                grades.Add(grade);
            }
            else
            {
                Console.WriteLine("You can only add grades between 0 and 100.");
            }
        }

        public Statistics  ComputeStatistics()
        {
            var stats = new Statistics();

            stats.High = grades.Any() ? HighGrade() : double.MaxValue;
            stats.Low = grades.Any() ? LowGrade() : double.MinValue;
            stats.Average = grades.Any() ? Average() : 0;

            return stats;
        }

        private double Average()
        {
            return grades.Average();
        }

        private double HighGrade()
        {
            return grades.Max();
        }

        private double LowGrade()
        {
            return grades.Min();
        }
    }
}
