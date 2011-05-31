using System;

class EmptyDequeException : Exception
{
    public EmptyDequeException()
        : base()
    {
    }
}

class DequeItem<T>
{
    public T value;
    public DequeItem<T> next;
    public DequeItem<T> prev;

    public DequeItem(T val)
    {
        next = prev = null;
        value = val;
    }
}

class Deque<T>
{
    DequeItem<T> beg, end;

    public void AddFront(T val)
    {
        if (beg == null)
            beg = end = new DequeItem<T>(val);
        else
        {
            DequeItem<T> elem = new DequeItem<T>(val);
            elem.next = beg;
            beg.prev = elem;
            beg = elem;
        }
    }

    public void AddBack(T val)
    {
        if (beg == null)
            beg = end = new DequeItem<T>(val);
        else
        {
            DequeItem<T> elem = new DequeItem<T>(val);
            end.next = elem;
            elem.prev = end;
            end = elem;
        }
    }

    public T PopFront()
    {
        if (beg == null)
            throw new EmptyDequeException();
        else
        {
            T value = beg.value;
            beg = beg.next;
            beg.prev = null;
            return value;
        }
    }


    public T PopBack()
    {
        if (beg == null)
            throw new EmptyDequeException();
        else
        {
            T value = end.value;
            end = end.prev;
            end.next = null;
            return value;
        }
    }

    public override string ToString()
    {
        DequeItem<T> cur = beg;
        string Result = "";
        while (cur != null)
        {
            cur = cur.next;
            Result += cur.value.ToString(); 
        }
        return Result;
    }
}
