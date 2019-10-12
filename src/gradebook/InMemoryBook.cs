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
                GradeAdded(this, new EventArgs() { });
            }
        }

        public override Statistics ComputeStatistics()
        {
            return ComputeStatistics(grades);
        }
    }
}
