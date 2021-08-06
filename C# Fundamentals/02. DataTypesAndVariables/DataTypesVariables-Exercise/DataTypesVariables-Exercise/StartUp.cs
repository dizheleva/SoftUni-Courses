using System;

namespace DataTypesVariables_Exercise
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            int c = int.Parse(Console.ReadLine());
            int d = int.Parse(Console.ReadLine());

            long result = ((long) a + b) / c * d; 

            Console.WriteLine(result);
        }
    }
}
