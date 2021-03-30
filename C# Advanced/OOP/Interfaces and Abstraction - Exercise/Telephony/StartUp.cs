using System;

namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var sites = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var smartphone = new Smartphone();
            var stationaryPhone = new StationaryPhone();

            foreach (var number in numbers)
            {

                if (number.Length == 7)
                {
                    stationaryPhone.Call(number);
                }
                else
                {
                    smartphone.Call(number);
                }
            }

            foreach (var site in sites)
            {
                smartphone.Browse(site);
            }
        }
    }
}
