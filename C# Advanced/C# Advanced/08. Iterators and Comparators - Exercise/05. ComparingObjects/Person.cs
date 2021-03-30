using System;

namespace ComparingObjects
{
    public class Person : IComparable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Town { get; set; }

        public Person(string name, int age, string town)
        {
            this.Name = name;
            this.Age = age;
            this.Town = town;
        }

        public int CompareTo(Person person)
        {
            return this.Name != person.Name ? this.Name.CompareTo(person.Name) 
                : this.Age != person.Age ? this.Age.CompareTo(person.Age) 
                : this.Town.CompareTo(person.Town);
        }
    }
}
