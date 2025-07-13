namespace _02.SumEvensInRange
{
    using System;

    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var command = Console.ReadLine();

                if (command == "show")
                {
                    var result = SumAsync();
                    Console.WriteLine(result);
                }
            }
        }

        private static long SumAsync()
        {
            return Task.Run(() =>
            {                
                long sum = 0;
                for (int i = 1; i < 1000; i++)
                {
                    if (i % 2 == 0)
                    {
                        sum += i;
                    }
                }
                return sum;
            }).Result; // Blocking call to wait for the task to complete
        }
    }
}
