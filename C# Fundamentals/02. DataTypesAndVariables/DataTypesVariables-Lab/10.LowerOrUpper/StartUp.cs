using System;

namespace _10.LowerOrUpper
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string character = Console.ReadLine();
            string smallChar = character.ToLower();

            if (smallChar == character)
            {
                Console.WriteLine("lower-case");
            }
            else
            {
                Console.WriteLine("upper-case");
            }
        }
    }
}
