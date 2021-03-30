using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Songs_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            var songs = new Queue<string>(Console.ReadLine()
                .Split(", "));

            while (songs.Any())
            {
                var command = Console.ReadLine();
                if (command.Contains("Play")) songs.Dequeue();
                else if (command.Contains("Add"))
                {
                    if (songs.Contains(command.Substring(4)))
                    {
                        Console.WriteLine($"{command.Substring(4)} is already contained!");
                    }
                    else
                    {
                        songs.Enqueue(command.Substring(4));
                    }
                }
                else if (command.Contains("Show"))
                {
                    Console.WriteLine(String.Join(", ", songs));
                }
            }

            Console.WriteLine("No more songs!");
        }
    }
}
