using System;
using System.Linq;

namespace Collection
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var listyIterator = new ListyIterator<string>();

            var input = Console.ReadLine();
            var data = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .ToList();
            listyIterator.Create(data);

            while ((input = Console.ReadLine()) != "END")
            {
                switch (input)
                {
                    case "Move":
                        Console.WriteLine(listyIterator.Move());
                        break;
                    case "HasNext":
                        Console.WriteLine(listyIterator.HasNext());
                        break;
                    case "Print":
                        listyIterator.Print();
                        break;
                    case "PrintAll":
                        listyIterator.PrintAll();
                        break;
                }
            }
        }
    }
}
