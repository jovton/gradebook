using System.Collections.Generic;

namespace GradeBook
{
    public class Statistics
    {
        private double average;
        public double Average
        {
            get
            {
                return average;
            }
            internal set
            {
                average = value;

                switch (average)
                {
                    case var d when d >= 90:
                        Letter = 'A';
                        break;

                    case var d when d >= 80:
                        Letter = 'B';
                        break;

                    case var d when d >= 70:
                        Letter = 'C';
                        break;

                    case var d when d >= 60:
                        Letter = 'D';
                        break;

                    default:
                        Letter = 'F';
                        break;
                }

            }
        }
        public double Low { get; internal set; }
        public double High { get; internal set; }
        public char Letter { get; private set; }
    }
}