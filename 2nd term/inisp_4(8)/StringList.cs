

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
