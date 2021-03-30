using System;
using System.Collections.Generic;
using System.Text;

namespace _02._Generic_Box_of_Integer
{
    public class Box<T> where T : IComparable
    {
        public Box(List<T> value)
        {
            this.Values = value;
        }
        public List<T> Values { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in Values)
            {
                sb.AppendLine($"{item.GetType()}: {item}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
