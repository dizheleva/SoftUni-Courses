using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class Family
    {
        private List<Person> people = new List<Person>();

        public List<Person> People
        {
            get { return people; }
            set { people = value; }
        }

        public void AddMember(Person member)
        {
            this.People.Add(member);
        }

        public Person GetOldestMember()
        {
            var oldestMember = this.People[0];

            foreach (var person in this.People)
            {
                if (person.Age > oldestMember.Age)
                {
                    oldestMember = person;
                }
            }

            return oldestMember;
        }

        public List<Person> GetOlderThan30()
        {
            var members = this.people.Where(members=>members.Age>30).OrderBy(member=>member.Name).ToList();
            
            return members;
        }
    }
}
