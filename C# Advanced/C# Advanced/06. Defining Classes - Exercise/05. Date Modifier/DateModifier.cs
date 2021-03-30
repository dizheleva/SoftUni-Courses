using System;
using System.Linq;

namespace DefiningClasses2
{
    public class DateModifier
    {
        private string firstDate;
        private string secondDate;

        public DateModifier(string firstDate, string secondDate)
        {
            FirstDate = firstDate;
            SecondDate = secondDate;

            var firstDateInfo = firstDate
                .Split()
                .Select(int.Parse)
                .ToArray();

            var firstDateTime = new DateTime(firstDateInfo[0], firstDateInfo[1], firstDateInfo[2]);

            var secondDateInfo = secondDate
                .Split()
                .Select(int.Parse)
                .ToArray();

            var secondDateTime = new DateTime(secondDateInfo[0], secondDateInfo[1], secondDateInfo[2]);

            var diff = secondDateTime - firstDateTime;
            Console.WriteLine(Math.Abs(diff.Days));
        }

        public string FirstDate
        {
            get { return firstDate; }
            set { firstDate = value; }
        }

        public string SecondDate
        {
            get { return secondDate; }
            set { secondDate = value; }
        }
    }
}
