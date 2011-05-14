using System;

public class ListItem
{
    public ListItem Next{ get; set; }
    public object Data { get; set; }

    public ListItem(object val)
    {
        Data = val;
    }
}


abstract public class List
{
    protected ListItem beg, end;

    public void AddBack(object item)
    {
        ListItem NewNode = new ListItem(item);
        if (beg == null)
            beg = end = NewNode;
        else
        {
            end.Next = NewNode;
            end = NewNode;
        }
    }

    public void AddFront(object item)
    {
        ListItem NewNode = new ListItem(item);
        if (beg == null)
            beg = end = NewNode;
        else
        {
            NewNode.Next = beg;
            beg = NewNode;
        }
    }

    public object GetFront()
    {
        return beg.Data;
    }

    public object GetBack()
    {
        return end.Data;
    }


    public void Print()
    {
        ListItem CurNode = beg;
        while (CurNode != null)
        {
            Console.Write(CurNode.Data + " ");
            CurNode = CurNode.Next;
        }
    }

}


class IntList : List
{
    public void AddBack(int item)
    {
        base.AddBack(item);
    }

    public void AddFront(int item)
    {
        base.AddFront(item);
    }

    public int GetFront()
    {
        return (int)base.GetFront();
    }

    public int GetBack()
    {
        return (int)base.GetBack();
    }

    public bool Find(int val)
    {
        ListItem CurNode = beg;
        while(CurNode != null)
        {
            if((int)CurNode.Data == val)
                return true;
            CurNode = CurNode.Next;
        }
        return false;
    }
}


class StringList : List
{
    public void AddBack(string item)
    {
        base.AddBack(item);
    }

    public void AddFront(string item)
    {
        base.AddFront(item);
    }

    public string GetFront()
    {
        return (string)base.GetFront();
    }

    public string GetBack()
    {
        return (string)base.GetBack();
    }

    public bool Find(string val)
    {
        ListItem CurNode = beg;
        while(CurNode != null)
        {
            if((string)CurNode.Data == val)
                return true;
            CurNode = CurNode.Next;
        }
        return false;
    }
}
