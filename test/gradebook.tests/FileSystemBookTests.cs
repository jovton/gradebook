using System;
using Xunit;
using GradeBook;
using System.IO;

namespace GradeBook.Tests
{
    public class FileSystemBookTests
    {
        const string bookName = "FileSystemBookTests";
        static readonly string fileName = $"{bookName}.txt";
        
        [Fact]
        public void NewFileSystemBookCreatesFile()
        {
            // arrange
            File.Delete(fileName);

            // act
            var book = new FileSystemBook(bookName);

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

                private int gradeAddedEventCounter;

        [Fact]
        public void AddGradeRaisesGradeAddedEvent()
        {
            // arrange
            var book = new FileSystemBook(bookName);
            book.GradeAdded += GradeAdded;
            gradeAddedEventCounter = 0;

            // act
            book.AddGrade(1);

            // assert
            Assert.Equal(1, gradeAddedEventCounter);
        }

        private void GradeAdded(object source, EventArgs args)
        {
            gradeAddedEventCounter++;
        }

        [Fact]
        public void ExistingBookFileTest()
        {
            // arrange
            File.Delete(fileName);
            File.WriteAllText(fileName, "44 55 66 B A 77");
            // act
            var book = new FileSystemBook(bookName);

            // assert
            Assert.True(book.HasGrades);
        }

        [Fact]
        public void ExistingBookInvalidFileTest()
        {
            // arrange
            File.Delete(fileName);
            File.WriteAllText(fileName, "44 H // @$% 55 66 B A 77");
            
            // act / assert
            Assert.Throws<FileLoadException>(() => new FileSystemBook(bookName));
        }
    }
}
