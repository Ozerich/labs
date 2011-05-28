
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

