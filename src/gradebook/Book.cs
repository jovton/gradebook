using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook
{
    public class Book
    {
        private List<double> grades;
        
        public string Name { get; set; }
        public bool HasGrades => grades.Any();

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
                throw new ArgumentException("ERROR: Invalid grade. Grades can only be between 0 and 100.", nameof(grade));
            }
        }

        public void AddLetterGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;

                case 'D':
                    AddGrade(60);
                    break;

                case 'F':
                    AddGrade(50);
                    break;

                default:
                    throw new ArgumentException($"Error: Invalid letter grade '{letter}'. Grades can only be from A to D, or F.");
            }
        }

        public Statistics  ComputeStatistics()
        {
            var stats = new Statistics();

            stats.High = HighGrade();
            stats.Low = LowGrade();
            stats.Average = Average();

            return stats;
        }

        private double Average()
        {
            double average = 0;

            if (grades.Any())
            {
                var index = 0;
                
                do
                {
                        average += grades[index];
                        index++;

                } while (index < grades.Count);
            
                average /= grades.Count;
            }

            return average;
        }

        private double HighGrade()
        {
            var high = HasGrades ? double.MinValue : 0d;
            var index = 0;
            
            while (index < grades.Count)
            {
                high = Math.Max(grades[index], high);
                index++;
            }

            return high;
        }

        private double LowGrade()
        {
            var low = HasGrades ? double.MaxValue : 0d;
            
            for (var index = 0; index < grades.Count; index++)
            {
                low = Math.Min(grades[index], low);
                index++;
            }

            return low;
        }
    }
}
