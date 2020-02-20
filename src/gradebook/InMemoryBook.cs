using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook
{
    public class InMemoryBook : Book
    {
        private List<double> _grades;

        public override bool HasGrades => _grades.Any();

        public InMemoryBook(string name) : base(name)
        {
            _grades = new List<double>();
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            EnsureValidGrade(grade);
            _grades.Add(grade);

            if (GradeAdded != null)
            {
                // ReSharper disable once PolymorphicFieldLikeEventInvocation
                GradeAdded(this, new EventArgs());
            }
        }

        public override Statistics ComputeStatistics()
        {
            return ComputeStatistics(_grades);
        }
    }
}
