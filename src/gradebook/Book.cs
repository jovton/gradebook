using System;
using System.Linq;

namespace GradeBook
{
    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name) { }
        
        public abstract bool HasGrades { get; }
        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract void AddGrade(char letter);
        public abstract Statistics ComputeStatistics();

        protected void EnsureValidGrade(double grade)
        {
            if (grade < 0 || grade > 100)
            {
                throw new ArgumentException("ERROR: Invalid grade. Grades can only be between 0 and 100.", nameof(grade));
            }
        }
        
        protected static readonly char[] ValidLetterGrades = new[] { 'A', 'B', 'C', 'D', 'F' };

        protected void EnsureValidGrade(char letter)
        {
            if (!ValidLetterGrades.Contains(letter))
            {
                throw new ArgumentException($"Error: Invalid letter grade '{letter}'. Grades can only be from A to D, or F.");
            }
        }
    }
}