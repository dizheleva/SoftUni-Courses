using System;

namespace CustomLinkedList
{
    public class DoublyLinkedList<T>
    {
        private class ListNode
        {
            public ListNode(T value)
            {
                this.Value = value;
            }

            public ListNode NextNode { get; set; }

            public ListNode PreviousNode { get; set; }

            public T Value { get; set; }
        }

        private ListNode head;
        private ListNode tail;

        public int Count { get; private set; }

        public void AddFirst(T element)
        {
            var newHead = new ListNode(element);

            if (this.Count == 0)
            {
                this.head = this.tail = newHead;
            }
            else
            {
                newHead.NextNode = this.head;
                this.head.PreviousNode = newHead;
                this.head = newHead;
            }

            this.Count++;
        }

        public void AddLast(T element)
        {
            var newTail = new ListNode(element);

            if (this.Count == 0)
            {
                this.tail = this.head = newTail;
            }
            else
            {
                newTail.PreviousNode = this.tail;
                this.tail.NextNode = newTail;
                this.tail = newTail;
            }

            this.Count++;
        }

        public T RemoveFirst()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The list is empty!");
            }

            var firstElement = this.head.Value;

            var head = this.head.NextNode;

            if (this.head != null)
            {
                this.head.PreviousNode = null;
            }
            else
            {
                this.tail = null;
            }

            this.Count--;
            return firstElement;
        }

        public T RemoveLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The list is empty!");
            }

            var lastElement = this.tail.Value;

            this.tail = this.tail.PreviousNode;

            if (this.tail != null)
            {
                this.tail.NextNode = null;
            }
            else
            {
                this.head = null;
            }

            this.Count--;
            return lastElement;
        }

        public void ForEach(Action<T> action)
        {
            var currentNode = this.head;
            
            while (currentNode != null)
            {
                action(currentNode.Value);

                currentNode = currentNode.NextNode;
            }
        }

        public void Clear()
        {
            this.head = null;
            this.tail = null;

            this.Count = 0;
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];
            var currentNode = this.head;

            for (var i = 0; i < this.Count; i++)
            {
                array[i] = currentNode.Value;
                currentNode = currentNode.NextNode;
            }

            return array;
        }
    }
}
