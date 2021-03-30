using System;
using System.Collections;
using System.Collections.Generic;

namespace _01._ListyIterator
{
    public class ListyIterator<T> : IEnumerable<T>
    {
        private List<T> items;
        private int index;
        public ListyIterator()
        {
            this.items = new List<T>();
            index = 0;
        }
        public void Create(List<T> list)
        {
            if (list.Count>0)
            {
                this.items = list;
                index = 0;
            }
            else
            {
                index = -1;
            }
        }
        public bool Move()
        {
            if (!HasNext()) return false;
            index++;
            return true;
        }
        public void Print()
        {
            if (index < 0 || index >= items.Count)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }
            Console.WriteLine(items[index]);
        }
        public bool HasNext()
        {
            return index < items.Count - 1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
