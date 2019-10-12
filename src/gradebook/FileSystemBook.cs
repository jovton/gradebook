using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradeBook
{
    public class FileSystemBook : Book
    {
        private readonly string fileName;

        private readonly List<double> grades = new List<double>();

        public override bool HasGrades => grades.Any();

        public FileSystemBook(string name) : base(name)
        {
            fileName = $"{name}_grades.txt";
            File.AppendAllText(fileName, "");
            RefreshGradesFromFile();
        }

        private void RefreshGradesFromFile()
        {
            var fileContent = File.ReadAllText(fileName);
            var fileGradesArray = fileContent.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            
            var gradesBackupArray = new double[grades.Count];
            grades.CopyTo(0, gradesBackupArray, 0, grades.Count);
            grades.Clear();

            foreach (var fileGrade in fileGradesArray)
            {
                try
                {
                    if (double.TryParse(fileGrade, out double grade))
                    {
                        EnsureValidGrade(grade);
                        grades.Add(grade);
                    }
                    else if (fileGrade.Length == 1)
                    {
                        EnsureValidGrade(fileGrade[0]);
                        var validLetterGrade = fileGrade.ToString().ToUpper()[0];
                        grades.Add(ValidLetterGrades[validLetterGrade]);
                    }
                    else
                    {
                        throw new FileLoadException($"ERROR: The grade book file contains an invalid grade '{fileGrade}'.");
                    }
                }
                catch
                {
                    grades.AddRange(gradesBackupArray);
                    throw;
                }
            }
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            EnsureValidGrade(grade);
            string gradeString = GetGradeString(grade);
            File.AppendAllText(fileName, $"{gradeString}");
            grades.Add(grade);
        }

        private string GetGradeString(double grade)
        {
            string gradeString = $"{grade}";
            var fileSize = new FileInfo(fileName).Length;

            if (fileSize > 0)
            {
                gradeString = " " + gradeString;
            }

            return gradeString;
        }

        public override Statistics ComputeStatistics()
        {
            return ComputeStatistics(grades);
        }
    }
}
