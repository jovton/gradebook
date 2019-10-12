using System;
using System.IO;

namespace GradeBook
{
    public class FileSystemBook : Book
    {
        public FileSystemBook(string name) : base(name)
        {
            var writer = File.AppendText($"{name}_grades.txt");
            writer.Close();
        }

        public override bool HasGrades => throw new NotImplementedException();

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            EnsureValidGrade(grade);
            throw new NotImplementedException();
        }

        public override void AddGrade(char letter)
        {
            EnsureValidGrade(letter);
            throw new NotImplementedException();
        }

        public override Statistics ComputeStatistics()
        {
            throw new NotImplementedException();
        }
    }
}
