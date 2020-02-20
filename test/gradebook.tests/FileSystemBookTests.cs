using System;
using Xunit;
using System.IO;

namespace GradeBook.Tests
{
    public class FileSystemBookTests
    {
        const string BookName = "FileSystemBookTests";
        static readonly string FileName = $"{BookName}.txt";
        
        [Fact]
        public void NewFileSystemBookCreatesFile()
        {
            // arrange
            File.Delete(FileName);

            // act
            // ReSharper disable once ObjectCreationAsStatement
            new FileSystemBook(BookName);

            // assert
            Assert.True(File.Exists(FileName));
        }

        [Fact]
        public void CannotAddOutside0To100()
        {
            // arrange
            File.Delete(FileName);
            var book = new FileSystemBook(BookName);

            // act / assert
            Assert.Throws<ArgumentException>(() => book.AddGrade(-1));
            Assert.Throws<ArgumentException>(() => book.AddGrade(100.0001));
            Assert.False(book.HasGrades);
        }

        [Fact]
        public void CannotAddInvalidLetterGrade()
        {
            // arrange
            File.Delete(FileName);
            var book = new FileSystemBook(BookName);

            // act / assert
            Assert.Throws<ArgumentException>(() => book.AddGrade('L'));
            Assert.False(book.HasGrades);
        }

        [Fact]
        public void AddingOneGradeAddsToFile()
        {
            // arrange
            File.Delete(FileName);
            var book = new FileSystemBook(BookName);

            // act
            book.AddGrade(10);

            // assert
            var fileContent = File.ReadAllText(FileName);
            var grade = double.Parse(fileContent);
            Assert.Equal(10, grade);
        }

        [Fact]
        public void AddingManyGradesAddsToFile()
        {
            // arrange
            File.Delete(FileName);
            var book = new FileSystemBook(BookName);

            // act
            book.AddGrade(20);
            book.AddGrade(30);

            // assert
            var fileContent = File.ReadAllText(FileName);            
            Assert.Equal("20 30", fileContent);
        }

        [Fact]
        public void NewEmptyBookHasNoGrades()
        {
            // arrange / act
            File.Delete(FileName);
            var book = new FileSystemBook(BookName);
            
            // assert
            Assert.False(book.HasGrades);
        }

        [Fact]
        public void AddingValidGradeMakesHasGradesTrue()
        {
            // arrange
            File.Delete(FileName);
            var book = new FileSystemBook(BookName);
            
            // act
            book.AddGrade(12);

            // assert
            Assert.True(book.HasGrades);
        }

        [Fact]
        public void CanComputeStatistics()
        {
            // arrange
            File.Delete(FileName);
            var book = new FileSystemBook(BookName);
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

                private int _gradeAddedEventCounter;

        [Fact]
        public void AddGradeRaisesGradeAddedEvent()
        {
            // arrange
            var book = new FileSystemBook(BookName);
            book.GradeAdded += GradeAdded;
            _gradeAddedEventCounter = 0;

            // act
            book.AddGrade(1);

            // assert
            Assert.Equal(1, _gradeAddedEventCounter);
        }

        private void GradeAdded(object source, EventArgs args)
        {
            _gradeAddedEventCounter++;
        }

        [Fact]
        public void ExistingBookFileTest()
        {
            // arrange
            File.Delete(FileName);
            File.WriteAllText(FileName, "44 55 66 B A 77");
            // act
            var book = new FileSystemBook(BookName);

            // assert
            Assert.True(book.HasGrades);
        }

        [Fact]
        public void ExistingBookInvalidFileTest()
        {
            // arrange
            File.Delete(FileName);
            File.WriteAllText(FileName, "44 H // @$% 55 66 B A 77");
            
            // act / assert
            Assert.Throws<FileLoadException>(() => new FileSystemBook(BookName));
        }
    }
}
