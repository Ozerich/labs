
public class ListItem
{
    public ListItem Next{ get; set; }
    public object Data { get; set; }

    public ListItem(object val)
    {
        Data = val;
    }
}

