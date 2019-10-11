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
            var result = book.ComputeStatistics();

            // assert
            Assert.Equal(40.27, result.Average, 2);
            Assert.Equal(30.1, result.Low);
            Assert.Equal(50.5, result.High);
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
            Assert.Equal(double.MaxValue, stats.High, 1);
            Assert.Equal(double.MinValue, stats.Low, 1);
            Assert.Equal(0, stats.Average, 1);
        }
    }
}
