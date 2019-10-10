using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook
{
    class Book
    {
        private List<double> grades;
        private string name;

        public Book(string name)
        {
            this.name = name;

            grades = new List<double>();
        }

        public void AddGrade(double grade)
        {
            grades.Add(grade);
        }

        public double Average()
        {
            return grades.Average();
        }

        internal void ShowStatistics()
        {
            var computationString = string.Join(" and ", grades.Select(n => $"{n}"));

            var avgMessage = $"The average grade of {computationString} is {Average():N1}.";            
            Console.WriteLine(avgMessage);

            var lowMessage = $"The lowest grade of {computationString} is {LowGrade():N1}.";
            Console.WriteLine(lowMessage);

            var highMessage = $"The highest grade of {computationString} is {HighGrade():N1}.";
            Console.WriteLine(highMessage);
        }

        public double HighGrade()
        {
            return grades.Max();
        }

        public double LowGrade()
        {
            return grades.Min();
        }
    }
}
