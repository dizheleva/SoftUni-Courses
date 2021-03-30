using System;
using System.Collections.Generic;

namespace EqualityLogic
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var sortedSet = new SortedSet<Person>();
            var hashSet = new HashSet<Person>();

            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; i++)
            {
                var personInfo = Console.ReadLine().Split();
                var person = new Person(personInfo[0], int.Parse(personInfo[1]));

                sortedSet.Add(person);
                hashSet.Add(person);
            }

            Console.WriteLine(sortedSet.Count);
            Console.WriteLine(hashSet.Count);
        }
    }
}
