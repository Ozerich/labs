using System;
using System.Collections.Generic;

interface IHasValue
{
    int val { get; set; }
}

class TVertex
{
    int id;
    public TVertex(int _id)
    {
        id = _id;
    }
}

class TValueVertex : TVertex, IHasValue 
{
    virtual public int val{get; set;}
    public TValueVertex(int id, int _val): base(id)
    {
        val = _val;
    }
}

class TEdge
{
    public int from_id { get; set; }
    public int to_id { get; set; }

    public TEdge(int from, int to)
    {
        from_id = from;
        to_id = to;
    }
}

class TValueEdge : TEdge, IHasValue
{
    virtual public int val { get; set; }
    public TValueEdge(int from, int to, int _val)
        : base(from, to)
    {
        val = _val;
    }
}

class TValueEdgeGraph
{
    private List<TValueEdge> edges;
    private List<TVertex> vertexes;

    void AddEdge(int from, int to, int val)
    {
        TValueEdge edge = new TValueEdge(from, to, val);
        edges.Add(edge);
    }

    void AddVertex(int id)
    {
        TVertex vertex = new TVertex(id);
        vertexes.Add(vertex);
    }
}


class TValueVertexGraph
{
    private List<TEdge> edges;
    private List<TValueVertex> vertexes;    

    void AddEdge(int from, int to)
    {
        TEdge edge = new TEdge(from, to);
        edges.Add(edge);
    }

    void AddVertex(int id, int val)
    {
        TValueVertex vertex = new TValueVertex(id, val);
        vertexes.Add(vertex);
    }
}


