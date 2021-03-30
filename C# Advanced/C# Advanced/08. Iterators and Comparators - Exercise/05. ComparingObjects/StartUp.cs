using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparingObjects
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var people = new List<Person>();
            var input = string.Empty;

            while ((input = Console.ReadLine()) != "END")
            {
                var personInfo = input.Split();
                var person = new Person(personInfo[0], int.Parse(personInfo[1]), personInfo[2]);
                people.Add(person);
            }

            var place = int.Parse(Console.ReadLine());
            var targetPerson = people[place - 1];
            var matches = people.Count(person => person.CompareTo(targetPerson) == 0);

            Console.WriteLine(matches == 1 ? "No matches" : $"{matches} {people.Count - matches} {people.Count}");
        }
    }
}
