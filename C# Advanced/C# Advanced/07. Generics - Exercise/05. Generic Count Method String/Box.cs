using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _05_Generic_Count_Method_String
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
        public int CountElements(List<T> list, T element)
        {
            return list.Count(item => element.CompareTo(item) < 0);
        }
    }
}
