using System;
using Xunit;

namespace GradeBook.Tests
{
    public class InMemoryBookTests
    {
        [Fact]
        public void ComputeStatistics()
        {
            // arrange
            var book = new InMemoryBook("Test Book");
            book.AddGrade(30.1);
            book.AddGrade(40.2);
            book.AddGrade(50.5);

            // act
            var stats = book.ComputeStatistics();

            // assert
            Assert.Equal(40.27, stats.Average, 2);
            Assert.Equal(30.1, stats.Low);
            Assert.Equal(50.5, stats.High);
            Assert.Equal('F', stats.Letter);
        }

        [Fact]
        public void CannotAddOutside0To100()
        {
            // arrange
            var book = new InMemoryBook("Test Book");

            // act / assert
            Assert.Throws<ArgumentException>(() => book.AddGrade(-1));
            Assert.Throws<ArgumentException>(() => book.AddGrade(1000));
            Assert.False(book.HasGrades);
        }

        [Fact]
        public void CannotAddInvalidLetterGrade()
        {
            // arrange
            var book = new InMemoryBook("Test Book");

            // act / assert
            Assert.Throws<ArgumentException>(() => book.AddGrade('/'));
            Assert.False(book.HasGrades);
        }

        [Fact]
        public void CanAddLetterGrade()
        {
            // arrange
            var book = new InMemoryBook("Letters");

            // act
            book.AddGrade('B');
            var stats = book.ComputeStatistics();

            // assert
            Assert.Equal(80, stats.Average);
        }

        [Fact]
        public void CanSetAndGetName()
        {
            // arrange
            var book = new InMemoryBook("Hello");
            
            // act
            book.Name = "Test";

            // assert
            Assert.Equal("Test", book.Name);
        }

        [Fact]
        public void CannotSetEmptyName()
        {
            // arrange / act / assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                const string emptyName = "";
                // ReSharper disable once ObjectCreationAsStatement
                new InMemoryBook(emptyName);
            });
            
            Assert.Throws<InvalidOperationException>(() =>
            {
                var b = new InMemoryBook("null test");
                b.Name = null;
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                var b = new InMemoryBook("whitespace test");
                b.Name = "      ";
            });
        }

        private int _gradeAddedEventCounter;

        [Fact]
        public void AddGradeRaisesGradeAddedEvent()
        {
            // arrange
            var book = new InMemoryBook("test");
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
        public void NewEmptyBookHasNoGrades()
        {
            // arrange / act
            var book = new InMemoryBook("test");
            
            // assert
            Assert.False(book.HasGrades);
        }

        [Fact]
        public void AddingGradesMakesHasGradesTrue()
        {
            // arrange
            var book = new InMemoryBook("test");
            
            // act
            book.AddGrade('A');

            // assert
            Assert.True(book.HasGrades);
        }
    }
}
