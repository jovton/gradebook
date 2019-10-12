using System;
using Xunit;
using GradeBook;
using System.IO;

namespace GradeBook.Tests
{
    public class FileSystemBookTests
    {
        [Fact]
        public void NewFileSystemBookCreatesFile()
        {
            // arrange
            const string bookName = "test";
            var fileName = $"{bookName}_grades.txt";
            File.Delete(fileName);

            // act
            var book = new FileSystemBook("test");

            // assert
            Assert.True(File.Exists(fileName));
        }
    }
}
