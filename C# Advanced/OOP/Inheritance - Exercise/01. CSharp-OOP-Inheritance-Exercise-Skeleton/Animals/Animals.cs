using System;
using System.Text;

namespace Animals
{
    public class Animal
    {
        private int age;
        private string name;

        public Animal(string name, int age, string gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Invalid input!");
                }

                this.name = value;
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Invalid input!");
                }

                age = value;
            }
        }

        public virtual string Gender { get; set; }

        public virtual string ProduceSound()
        {
            return null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(this.GetType().Name);
            sb.AppendLine($"{this.Name} {this.Age} {this.Gender}");
            sb.Append(this.ProduceSound());

            return sb.ToString().TrimEnd();
        }
    }
}
