using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook
{
    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name) { }

        public abstract bool HasGrades { get; }
        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);

        public virtual void AddGrade(char letter)
        {
            EnsureValidGrade(letter);
            AddGrade(ValidLetterGrades[letter.ToString().ToUpper()[0]]);
        }

        public abstract Statistics ComputeStatistics();

        protected void EnsureValidGrade(double grade)
        {
            if (grade < 0 || grade > 100)
            {
                throw new ArgumentException("ERROR: Invalid grade. Grades can only be between 0 and 100.", nameof(grade));
            }
        }

        protected static readonly Dictionary<char, int> ValidLetterGrades = new Dictionary<char, int>
        {
            { 'A', 90 },
            { 'B', 80 },
            { 'C', 70 },
            { 'D', 60 },
            { 'F', 50 }
        };

        protected void EnsureValidGrade(char letter)
        {
            if (!ValidLetterGrades.Any(g => g.Key == letter.ToString().ToUpper()[0]))
            {
                throw new ArgumentException($"Error: Invalid letter grade '{letter}'. Grades can only be from A to D, or F.");
            }
        }

        protected Statistics ComputeStatistics(IEnumerable<double> grades)
        {
            var stats = new Statistics();

            foreach (var grade in grades)
            {
                stats.Add(grade);
            }

            return stats;
        }
    }
}