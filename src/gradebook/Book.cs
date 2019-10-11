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
                    AddGrade(0);
                    break;
            }
        }

        public Statistics  ComputeStatistics()
        {
            var stats = new Statistics();

            stats.High = HighGrade();
            stats.Low = LowGrade();
            stats.Average = Average();

            switch (stats.Average)
            {
                case var d when d >= 90:
                    stats.Letter = 'A';
                    break;

                case var d when d >= 80:
                    stats.Letter = 'B';
                    break;

                case var d when d >= 70:
                    stats.Letter = 'C';
                    break;

                case var d when d >= 60:
                    stats.Letter = 'D';
                    break;

                default:
                    stats.Letter = 'F';
                    break;
            }

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

            return average; //grades.Average(); // sad face :(
        }

        private double HighGrade()
        {
            var high = double.MinValue;
            var index = 0;
            
            while (index < grades.Count)
            {
                high = Math.Max(grades[index], high);
                index++;
            }

            return high; // grades.Max(); // sad face :(
        }

        private double LowGrade()
        {
            var low = double.MaxValue;
            
            for (var index = 0; index < grades.Count; index++)
            {
                low = Math.Min(grades[index], low);
                index++;
            }

            return low; // grades.Min(); // sad face :(
        }
    }
}
