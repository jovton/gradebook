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

            // act / assert
            Assert.Throws<ArgumentException>(() => book.AddGrade(-1));
            Assert.False(book.HasGrades);
        }

        [Fact]
        public void CanAddLetterGrade()
        {
            // arrange
            var book = new Book("Letters");

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
            var book = new Book("Hello");
            
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
                new Book(emptyName);
            });
            
            Assert.Throws<InvalidOperationException>(() =>
            {
                var b = new Book("null test");
                b.Name = null;
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                var b = new Book("whitespace test");
                b.Name = "      ";
            });
        }
    }
}
