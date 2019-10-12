namespace GradeBook
{
    public interface IBook : INamedObject
    {
        bool HasGrades { get; }
        void AddGrade(double grade);
        void AddGrade(char letter);
        Statistics ComputeStatistics();
        event GradeAddedDelegate GradeAdded;
    }
}