using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string message);

    public class TypeTests
    {
        [Fact]
        public void CanSetNameByValue()
        {
            // arrange
            var book = GetBook("Book 1");

            // act
            SetNameByValue(book, "Test Book");

            // assert
            Assert.Equal("Test Book", book.Name);
        }

        private void SetNameByValue(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            // arrange
            var book = GetBook("Book 1");

            // act
            GetBookSetName(book, "Test Book 2");

            // assert
            Assert.Equal("Book 1", book.Name);
        }

        // ReSharper disable once RedundantAssignment
        // ReSharper disable once UnusedParameter.Local
        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
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

        private void GetBookByRef(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void NewBookReturnsDifferentInstance()
        {
            // arrange
            var book1 = GetBook("Test Book");

            // act
            var book2 = GetBook("Test Book 2");

            // assert
            Assert.Equal("Test Book", book1.Name);
            Assert.Equal("Test Book 2", book2.Name);
        }

        [Fact]
        public void NewBookDoesNotReturnSameInstance()
        {
            // arrange
            var book1 = GetBook("Test Book");

            // act
            var book2 = GetBook("Test Book 2");

            // assert
            Assert.NotEqual("Test Book", book2.Name);
            Assert.NotSame(book1, book2);
            Assert.False(ReferenceEquals(book1, book2));
        }

        [Fact]
        public void TwoVariablesCanReferenceSameInstance()
        {
            // arrange
            var book1 = GetBook("Test Book");
            
            // act
            var book2 = book1;

            // assert
            Assert.Same(book1, book2);
            Assert.True(ReferenceEquals(book1, book2));
        }

        private InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }

        [Fact]
        public void ValuesTypesAlsoPassByValue()
        {
            // arrange
            var x = GetInt();
            
            // act
            SetInt(x);

            // assert
            Assert.Equal(3, x);
        }

        // ReSharper disable once RedundantAssignment
        // ReSharper disable once UnusedParameter.Local
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
            // arrange
            var x = GetInt();

            // act
            SetIntByRef(ref x);

            // assert
            Assert.Equal(43, x);
        }

        private void SetIntByRef(ref int x)
        {
            x = 43;
        }

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            // arrange
            string name = "jovton";

            //act
            ChangeStringParameter(name);
            var upper = MakeUpperCase(name);

            // assert
            Assert.Equal("jovton", name);
            Assert.Equal("JOVTON", upper);
        }

        private void ChangeStringParameter(string str)
        {
            str = str.ToUpper();
        }

        private string MakeUpperCase(string str)
        {
            return str.ToUpper();
        }

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            // arrange
            WriteLogDelegate returnMessage = ReturnAMessage;
            
            // act
            var message = returnMessage("hello");

            // assert
            Assert.Equal("hello", message);
        }

        private string ReturnAMessage(string message)
        {
            return message;
        }

        private int _logCounter;
        
        [Fact]
        public void MultiCastDelegateTest()
        {
            // arrange
            WriteLogDelegate log = FirstLog;
            log += SecondLog;
            _logCounter = 0;

            // act
            log("hello");

            // assert
            Assert.Equal(2, _logCounter);
        }

        private string SecondLog(string message)
        {
            _logCounter++;
            return message;
        }

        private string FirstLog(string message)
        {
            _logCounter++;
            return message;
        }
    }
}
