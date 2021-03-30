using System;

namespace Tuple
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var personInfo = Console.ReadLine().Split();
            var personName = personInfo[0] + " " + personInfo[1];
            var address = personInfo[2];

            var beerInfo = Console.ReadLine().Split();
            var name = beerInfo[0];
            var amountOfBeer = int.Parse(beerInfo[1]);

            var numbers = Console.ReadLine().Split();
            var intNum = int.Parse(numbers[0]);
            var doubleNum = double.Parse(numbers[1]);

            var tuplePerson = new Tuple<string, string>(personName, address);
            var tupleBeer = new Tuple<string, int>(name, amountOfBeer);
            var tupleNumbers = new Tuple<int, double>(intNum, doubleNum);

            Console.WriteLine(tuplePerson.ToString());
            Console.WriteLine(tupleBeer.ToString());
            Console.WriteLine(tupleNumbers.ToString());
        }
    }
}
