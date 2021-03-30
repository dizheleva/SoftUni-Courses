using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Truck_Tour
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var petrol = new Queue<int>();
            var distance = new Queue<int>();

            for (var i = 0; i < n; i++)
            {
                var input = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                petrol.Enqueue(input[0]);
                distance.Enqueue(input[1]);
            }

            for (var i = 0; i < n; i++)
            {
                var currentFuel = petrol.Peek();
                var petrolCopy = new Queue<int>(petrol);
                var distanceCopy = new Queue<int>(distance);

                for (var x = 0; x < n; x++)
                {
                    if (distanceCopy.Peek() <= currentFuel)
                    {
                        currentFuel -= distanceCopy.Peek();
                        if (x == n - 1)
                        {
                            Console.WriteLine(i);
                            return;
                        }
                    }
                    else
                    {
                        break;
                    }

                    petrolCopy.Enqueue(petrolCopy.Dequeue());
                    distanceCopy.Enqueue(distanceCopy.Dequeue());
                    currentFuel += petrolCopy.Peek();
                }

                petrol.Enqueue(petrol.Dequeue());
                distance.Enqueue(distance.Dequeue());
            }
        }
    }
}
