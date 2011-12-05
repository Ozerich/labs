using System;


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
