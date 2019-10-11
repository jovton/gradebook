using System;
using Xunit;
using GradeBook;

namespace GradeBook.Tests
{
    public class TypeTests
    {
        [Fact]
        public void CanSetNameByValue()
        {
            // arrange / act
            var book = GetBook("Book 1");
            SetNameByValue(book, "Test Book");

            // assert
            Assert.Equal("Test Book", book.Name);
        }

        private void SetNameByValue(Book book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            // arrange / act
            var book = GetBook("Book 1");
            GetBookSetName(book, "Test Book 2");

            // assert
            Assert.Equal("Book 1", book.Name);
        }

        private void GetBookSetName(Book book, string name)
        {
            book = new Book(name);
        }

        [Fact]
        public void CSharpPassByReference()
        {
            // arrange / act
            var book = GetBook("Book 1");
            GetBookByRef(ref book, "Test Book 2");

            // assert
            Assert.Equal("Test Book 2", book.Name);
        }

        private void GetBookByRef(ref Book book, string name)
        {
            book = new Book(name);
        }

        [Fact]
        public void NewBookReturnsDifferentInstance()
        {
            // arrange / act
            var book1 = GetBook("Test Book");
            var book2 = GetBook("Test Book 2");

            // assert
            Assert.Equal("Test Book", book1.Name);
            Assert.Equal("Test Book 2", book2.Name);
        }

        [Fact]
        public void NewBookDoesNotReturnSameInstance()
        {
            // arrange / act
            var book1 = GetBook("Test Book");
            var book2 = GetBook("Test Book 2");

            // assert
            Assert.NotEqual("Test Book", book2.Name);
            Assert.NotSame(book1, book2);
            Assert.False(object.ReferenceEquals(book1, book2));
        }

        [Fact]
        public void TwoVariablesCanReferenceSameInstance()
        {
            // arrange / act
            var book1 = GetBook("Test Book");
            var book2 = book1;

            // assert
            Assert.Same(book1, book2);
            Assert.True(object.ReferenceEquals(book1, book2));
        }

        private Book GetBook(string name)
        {
            return new Book(name);
        }

        [Fact]
        public void ValuesTypesAlsoPassByValue()
        {
            var x = GetInt();
            SetInt(x);
            Assert.Equal(3, x);
        }

        private void SetInt(int x)
        {
            x = 43;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void ValuesTypesCanPassByRef()
        {
            var x = GetInt();
            SetIntByRef(ref x);
            Assert.Equal(43, x);
        }

        private void SetIntByRef(ref int x)
        {
            x = 43;
        }

    }
}
