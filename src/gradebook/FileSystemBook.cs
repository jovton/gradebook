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
            fileName = $"{name}.txt";
            File.AppendAllText(fileName, "");
            grades = ReadGradesFromFile();
        }

        private List<double> ReadGradesFromFile()
        {
            var tempGrades = new List<double>();
            var fileContent = File.ReadAllText(fileName);
            var fileGradesArray = fileContent.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var fileGrade in fileGradesArray)
            {
                var fileErrorMessage = $"ERROR: The grade book file contains an invalid grade '{fileGrade}'.";

                if (double.TryParse(fileGrade, out double grade))
                {
                    try
                    {
                        EnsureValidGrade(grade);
                    }
                    catch (ArgumentException)
                    {
                        throw new FileLoadException(fileErrorMessage);
                    }

                    tempGrades.Add(grade);
                }
                else if (fileGrade.Length == 1)
                {
                    try
                    {
                        EnsureValidGrade(fileGrade[0]);
                    }
                    catch (ArgumentException)
                    {
                        throw new FileLoadException(fileErrorMessage);
                    }
                    
                    var validLetterGrade = fileGrade.ToString().ToUpper()[0];
                    tempGrades.Add(ValidLetterGrades[validLetterGrade]);
                }
                else
                {
                    throw new FileLoadException(fileErrorMessage);
                }
            }

            return tempGrades;
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            EnsureValidGrade(grade);
            string gradeString = GetGradeString(grade);
            File.AppendAllText(fileName, $"{gradeString}");
            grades.Add(grade);

            if (GradeAdded != null)
            {
                GradeAdded(this, new EventArgs() { });
            }
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
