namespace GradeBook
{
    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name) { }
        
        public abstract bool HasGrades { get; }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract void AddGrade(char letter);

        public abstract Statistics ComputeStatistics();
    }
}