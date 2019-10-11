using System;
using Xunit;
using GradeBook;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void ComputeStatistics()
        {
            // arrange
            var book = new Book("Test Book");
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
        public void CannotAddNegative()
        {
            // arrange
            var book = new Book("Test Book");

            // act
            book.AddGrade(-1);
            var stats = book.ComputeStatistics();

            // assert
            Assert.Equal(double.MinValue, stats.High, 1);
            Assert.Equal(double.MaxValue, stats.Low, 1);
            Assert.Equal(0, stats.Average, 1);
        }

        [Fact]
        public void CanAddLetterGrade()
        {
            // arrange
            var book = new Book("Letters");

            // act
            book.AddLetterGrade('B');
            var stats = book.ComputeStatistics();

            // assert
            Assert.Equal(80, stats.Average);
        }
    }
}
