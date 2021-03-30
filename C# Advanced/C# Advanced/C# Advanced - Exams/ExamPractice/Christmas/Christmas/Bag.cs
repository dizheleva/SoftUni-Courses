using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Christmas
{
    public class Bag : Present
    {
        private HashSet<Present> data;
        public string Color { get; set; }
        public int Capacity { get; set; }
        public int Count => data.Count;

        private Bag()
        {
            data = new HashSet<Present>();
        }
        public Bag(string name, int capacity) : this()
        {
            Color = name;
            Capacity = capacity;
        }

        public void Add(Present present)
        {
            if (Capacity > data.Count)
            {
                data.Add(present);
            }
        }

        public bool Remove(string name)
        {
            var playerIsRemoved = data.Any(p => p.Name == name);
            if (playerIsRemoved)
            {
                data.Remove(data.First(p => p.Name == name));
            }
            return playerIsRemoved;
        }

        public Present GetHeaviestPresent()
        {
            return data.First(p => p.Weight == data.Select(present => present.Weight).Max());
        }

        public Present GetPresent(string name)
        {
            return data.First(p => p.Name == name);
        }
        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.Color} bag contains:");
            sb.AppendLine($"{ string.Join(Environment.NewLine, data)}");

            return sb.ToString().TrimEnd();
        }
    }
}
