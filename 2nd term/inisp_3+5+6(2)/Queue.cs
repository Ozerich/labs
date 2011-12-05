using System;

namespace laba3
{
    class EmptyQueue : Exception
    {
        public EmptyQueue() : base() { }
    }

    class QueueNode<T>
    {
        public QueueNode<T> next;
        public T value;

        public QueueNode(T val)
        {
            value = val;
        }
    }
    
    class Queue<T>
    {
        private QueueNode<T> begin, end, cur;

        public int Length
        {
            get 
            {
                cur = begin;
                int count = 0;
                while (cur != null)
                {
                    count++;
                    cur = cur.next;
                }
                return count;
            }
        }

        public bool Empty
        {
            get
            {
                return Length == 0;
            }
        }

        public void Push(T val)
        {
            cur = new QueueNode<T>(val);
            if (begin == null)
                begin = end = cur;
            else
            {
                end.next = cur;
                end = end.next;
            }
        }

        public T Front()
        {
            if (begin == null)
                throw new EmptyQueue();
            return begin.value;
        }

        public void Pop()
        {
            if (begin == null)
                throw new EmptyQueue();
            if (begin.next == null)
                begin = end = null;
            else
            {
                if (begin.next == end)
                    end = begin;
                begin = begin.next;
            }
        }

        public void Print()
        {
            if (begin == null)
                throw new EmptyQueue();
        
            cur = begin;
            while (cur != null)
            {
                Console.Write(cur.value + " ");
                cur = cur.next;
            }
            Console.WriteLine();
        }
    }
}

