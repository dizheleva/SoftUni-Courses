using System;

namespace DefiningClasses2
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var firstDate = Console.ReadLine();
            var secondDate = Console.ReadLine();

            var modifier = new DateModifier(firstDate, secondDate);
        }
    }
}
