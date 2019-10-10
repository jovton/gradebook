using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("jovton's Grade Book.");

            var grades = new List<double>() { 12.7, 4, 77.41, 5.123 };
            
            foreach (var grade in grades)
            {
                book.AddGrade(grade);
            }

            book.ShowStatistics();
        }
    }
}
