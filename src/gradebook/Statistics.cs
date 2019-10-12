using System;
using System.Collections.Generic;

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
                return sum / (count == 0 ? 1 : count);
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
            sum += grade;
            count++;
            High = count == 1 ? grade : Math.Max(grade, High);
            Low = count == 1 ? grade : Math.Min(grade, Low);
        }

        private double sum;
        private int count;
    }
}