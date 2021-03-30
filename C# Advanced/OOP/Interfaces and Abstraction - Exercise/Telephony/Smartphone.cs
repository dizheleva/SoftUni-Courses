using System;
using System.Linq;

namespace Telephony
{
    public class Smartphone : ICall, IBrowse
    {
        public Smartphone()
        {
        }
        
        public void Browse(string site)
        {
            if (site.Any(ch => char.IsDigit(ch)))
            {
                Console.WriteLine("Invalid URL!");
                return;
            }

            Console.WriteLine($"Browsing: {site}!");
        }

        public void Call(string number)
        {
            if (number.Any(ch => !char.IsDigit(ch)))
            {
                Console.WriteLine("Invalid number!");
                return;
            }

            Console.WriteLine($"Calling... {number}");
        }
    }
}
