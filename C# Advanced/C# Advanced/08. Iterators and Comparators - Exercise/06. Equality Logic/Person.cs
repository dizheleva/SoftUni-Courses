using System;

namespace EqualityLogic
{
    public class Person : IComparable<Person>
    {
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; set; }
        public int Age { get; set; }

        public int CompareTo(Person other)
        {
            return this.Name != other.Name ? this.Name.CompareTo(other.Name) : this.Age.CompareTo(other.Age);
        }

        public override bool Equals(object obj)
        {
            return obj is Person person && (this.Name == person.Name && this.Age == person.Age);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + this.Age.GetHashCode();
        }
    }
}
