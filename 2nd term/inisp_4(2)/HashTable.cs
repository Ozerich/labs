using System;

class HashItem
{
    public object Data{get;set;}
    public HashItem Next{get;set;}

    public HashItem(object val)
    {
        Data = val;
    }
}

abstract class HashTable
{
    protected HashItem[] Data;
    protected int HashSize;

    public HashTable(int H)
    {
        Data = new HashItem[H];
        HashSize = H;
    }

    abstract public int Hash(object val);

    public void Add(object val)
    {
        int key = Hash(val);
        if (Data[key] == null)
            Data[key] = new HashItem(val);
        else
        {
            HashItem Cur = new HashItem(val);
            Cur.Next = Data[key];
            Data[key] = Cur;
        }
    }

    public void Print()
    {
        HashItem Cur;
        for (int i = 0; i < HashSize; i++)
        {
            Console.Write(i + " - ");
            Cur = Data[i];
            while (Cur != null)
            {
                Console.Write(Cur.Data + " ");
                Cur = Cur.Next;
            }
            Console.WriteLine();
        }
    }


}

class IntHashTable : HashTable
{
    public IntHashTable(int h)
        : base(h)
    {
    }

    public override int Hash(object val)
    {
        int key = (int)val;
        return key % HashSize;
    }

    public void Add(int val)
    {
        base.Add((object)val);
    }

    public bool Find(int val)
    {
        int key = Hash(val);
        HashItem Cur = Data[key];
        while (Cur != null)
        {
            if ((int)Cur.Data == val)
                return true;
            Cur = Cur.Next;
        }
        return false;
    }

    public void Print()
    {
        HashItem Cur;
        for (int i = 0; i < HashSize; i++)
        {
            Console.Write(i + " - ");
            Cur = Data[i];
            while (Cur != null)
            {
                Console.Write(Cur.Data + " ");
                Cur = Cur.Next;
            }
            Console.WriteLine();
        }
    }
}

class StringHashTable : HashTable
{
    public StringHashTable(int h)
        : base(h)
    {
    }

    public override int Hash(object val)
    {
        int key = ((string)val).Length;
        return key % HashSize;
    }

    public void Add(string val)
    {
        base.Add((object)val);
    }

    public bool Find(string val)
    {
        int key = Hash(val);
        HashItem Cur = Data[key];
        while (Cur != null)
        {
            if ((string)Cur.Data == val)
                return true;
            Cur = Cur.Next;
        }
        return false;
    }
}