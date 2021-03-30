using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _06._Generic_Count_Method_Double
{
    public class Box<T> where T : IComparable
    {
        public Box(List<T> value)
        {
            this.Values = value;
        }
        public List<T> Values { get; set; }

        public int CountElements(List<T> list, T element)
        {
            return list.Count(item => element.CompareTo(item) < 0);
        }

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
