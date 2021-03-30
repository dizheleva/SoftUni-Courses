using System;
using System.Collections;
using System.Collections.Generic;

namespace Stack
{
    public class Stack<T> : IEnumerable<T>
    {
        private List<T> items;
        private int Count;
        public Stack()
        {
            this.items = new List<T>();
        }
        public void Push(List<T> list)
        {
            foreach (var item in list)
            {
                this.items.Add(item);
            }
        }
        public void Pop()
        {
            if (this.items.Count != 0)
            {
                this.items.RemoveAt(this.items.Count - 1);
            }
            else
            {
                Console.WriteLine("No elements");
                Environment.Exit(0);
            }
        }
        public void End()
        {            
            this.items.AddRange(this.items);
            for (var i = this.items.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(this.items[i]);
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = this.items.Count - 1; i >= 0; i--)
            {
                yield return this.items[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
