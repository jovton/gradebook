namespace GradeBook
{
    public interface IBook
    {
        bool HasGrades { get; }

        void AddGrade(double grade);
        void AddGrade(char letter);
        Statistics ComputeStatistics();
        event GradeAddedDelegate GradeAdded;
    }
}