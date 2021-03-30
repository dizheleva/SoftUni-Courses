using System;
using System.Collections.Generic;
using System.Linq;

namespace _10._Crossroads
{
    class Program
    {
        static void Main(string[] args)
        {
            var greenLightDurations = int.Parse(Console.ReadLine());
            var freeWindowDuration = int.Parse(Console.ReadLine());
            var cars = new Queue<string>();
            var carsPassed = 0;

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "END")
                {
                    Console.WriteLine($"Everyone is safe." +
                                      $"\n{carsPassed} total cars passed the crossroads.");
                    break;
                }
                if (input == "green")
                {
                    var currentTime = greenLightDurations;
                    while (cars.Any() && currentTime > 0)
                    {
                        var currentCar = cars.Any() ? cars.Dequeue() : String.Empty;
                        currentTime -= currentCar.Length;
                        if (currentTime >= 0)
                        {
                            carsPassed++;
                        }
                        else
                        {
                            currentTime += freeWindowDuration;
                            if (currentTime < 0)
                            {
                                Console.WriteLine($"A crash happened!" +
                                                  $"\n{currentCar} was hit at {currentCar[currentCar.Length + currentTime]}.");
                                return;
                            }
                            carsPassed++;
                            break;
                        }
                    }
                }
                else
                {
                    cars.Enqueue(input);
                }
            }
        }
    }
}
