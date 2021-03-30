using System;
using System.Linq;

namespace Telephony
{
    public class StationaryPhone : ICall
    {
        public StationaryPhone()
        {
        }

        public void Call(string number)
        {
            if (number.Any(ch => !char.IsDigit(ch)))
            {
                Console.WriteLine("Invalid number!");
                return;
            }

            Console.WriteLine($"Dialing... {number}");
        }
    }
}
