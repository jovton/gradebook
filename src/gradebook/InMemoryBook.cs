using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook
{
    public class InMemoryBook : Book
    {
        private List<double> grades;
        
        public override bool HasGrades => grades.Any();

        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
        }
        
        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            EnsureValidGrade(grade);
            grades.Add(grade);

            if (GradeAdded != null)
            {
                GradeAdded(this, new EventArgs() {  });
            }

        }

        public override void AddGrade(char letter)
        {
            EnsureValidGrade(letter);

            switch (letter.ToString().ToUpper()[0])
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
            }
        }

        public override Statistics  ComputeStatistics()
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

            if (HasGrades)
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