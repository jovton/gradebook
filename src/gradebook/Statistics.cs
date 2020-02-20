using System;

namespace GradeBook
{
    public class Statistics
    {
        public double Low { get; private set; }
        public double High { get; private set; }

        public double Average
        {
            get
            {
                return _sum / (_count == 0 ? 1 : _count);
            }
        }

        public char Letter
        {
            get
            {
                switch (Average)
                {
                    case var d when d >= 90:
                        return 'A';

                    case var d when d >= 80:
                        return 'B';

                    case var d when d >= 70:
                        return 'C';

                    case var d when d >= 60:
                        return 'D';

                    default:
                        return 'F';
                }
            }
        }

        public void Add(double grade)
        {
            _sum += grade;
            _count++;
            High = _count == 1 ? grade : Math.Max(grade, High);
            Low = _count == 1 ? grade : Math.Min(grade, Low);
        }

        private double _sum;
        private int _count;
    }
}