﻿namespace _03.SumEvensInBackground
{
    public class Program
    {
        static void Main(string[] args)
        {
            long sum = 0;

            var task = Task.Run(() =>
            {
                for (int i = 1; i < 1000000000; i++)
                {
                    if (i % 2 == 0)
                    {
                        sum += i;
                    }
                }
            });

            while (true)
            {
                var line = Console.ReadLine();

                if (line == "exit")
                {
                    break;
                }
                else if (line == "show")
                {
                    Console.WriteLine(sum);
                }
            }
        }
    }
}
