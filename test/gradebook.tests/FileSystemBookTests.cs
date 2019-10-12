using System;
using Xunit;
using GradeBook;
using System.IO;

namespace GradeBook.Tests
{
    public class FileSystemBookTests
    {
        const string bookName = "test";
        static readonly string fileName = $"{bookName}_grades.txt";
        
        [Fact]
        public void NewFileSystemBookCreatesFile()
        {
            // arrange
            File.Delete(fileName);

            // act
            var book = new FileSystemBook("test");

            // assert
            Assert.True(File.Exists(fileName));
        }

        [Fact]
        public void CannotAddOutside0to100()
        {
            // arrange
            File.Delete(fileName);
            var book = new FileSystemBook(bookName);

            // act / assert
            Assert.Throws<ArgumentException>(() => book.AddGrade(-1));
            Assert.Throws<ArgumentException>(() => book.AddGrade(100.0001));
            Assert.False(book.HasGrades);
        }

        [Fact]
        public void CannotAddInvalidLetterGrade()
        {
            // arrange
            File.Delete(fileName);
            var book = new FileSystemBook(bookName);

            // act / assert
            Assert.Throws<ArgumentException>(() => book.AddGrade('L'));
            Assert.False(book.HasGrades);
        }

        [Fact]
        public void AddingOneGradeAddsToFile()
        {
            // arrange
            File.Delete(fileName);
            var book = new FileSystemBook(bookName);

            // act
            book.AddGrade(10);

            // assert
            var fileContent = File.ReadAllText(fileName);
            var grade = double.Parse(fileContent);
            Assert.Equal(10, grade);
        }

        [Fact]
        public void AddingManyGradesAddsToFile()
        {
            // arrange
            File.Delete(fileName);
            var book = new FileSystemBook(bookName);

            // act
            book.AddGrade(20);
            book.AddGrade(30);

            // assert
            var fileContent = File.ReadAllText(fileName);            
            Assert.Equal("20 30", fileContent);
        }

        [Fact]
        public void NewEmptyBookHasNoGrades()
        {
            // arrange / act
            File.Delete(fileName);
            var book = new FileSystemBook(bookName);
            
            // assert
            Assert.False(book.HasGrades);
        }

        [Fact]
        public void AddingValidGradeMakesHasGradesTrue()
        {
            // arrange
            File.Delete(fileName);
            var book = new FileSystemBook(bookName);
            
            // act
            book.AddGrade(12);

            // assert
            Assert.True(book.HasGrades);
        }

        [Fact]
        public void CanComputeStatistics()
        {
            // arrange
            File.Delete(fileName);
            var book = new FileSystemBook(bookName);
            book.AddGrade(56.2);
            book.AddGrade(70.4);
            book.AddGrade(42.1);
            
            // act
            var stats = book.ComputeStatistics();

            // assert
            Assert.Equal(56.23, stats.Average, 2);
            Assert.Equal(42.1, stats.Low);
            Assert.Equal(70.4, stats.High);
            Assert.Equal('F', stats.Letter);
        }
    }
}
