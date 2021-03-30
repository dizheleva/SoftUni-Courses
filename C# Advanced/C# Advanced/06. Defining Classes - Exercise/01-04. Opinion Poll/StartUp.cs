using System;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var family = new Family();

            var count = int.Parse(Console.ReadLine());

            for (var i = 0; i < count; i++)
            {
                var personInfo = Console.ReadLine().Split();
                var name = personInfo[0];
                var age = int.Parse(personInfo[1]);

                var person = new Person(name, age);

                family.AddMember(person);
            }

            //Console.WriteLine($"{family.GetOldestMember().Name} {family.GetOldestMember().Age}");

            var peopleMoreThan30 = family.GetOlderThan30();
            
            foreach (var person in peopleMoreThan30)
            {
                Console.WriteLine($"{person.Name} - {person.Age}");
            }
        }
    }
}
