using System;

namespace Threeuple
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var personInfo = Console.ReadLine().Split();
            var personName = personInfo[0] + " " + personInfo[1];
            var address = personInfo[2];
            var town = personInfo[3];

            var beerInfo = Console.ReadLine().Split();
            var beerName = beerInfo[0];
            var amountOfBeer = int.Parse(beerInfo[1]);
            var condition = beerInfo[2] == "drunk" ? true : false;

            var bankInfo = Console.ReadLine().Split();
            var name = bankInfo[0];
            var balance = double.Parse(bankInfo[1]);
            var bankName = bankInfo[2];


            var threeuplePerson = new Threeuple<string, string, string>(personName, address, town);
            var threeupleBeer = new Threeuple<string, int, bool>(beerName, amountOfBeer, condition);
            var threeupleBank = new Threeuple<string, double, string>(name, balance, bankName);

            Console.WriteLine(threeuplePerson.ToString());
            Console.WriteLine(threeupleBeer.ToString());
            Console.WriteLine(threeupleBank.ToString());
        }
    }
}
