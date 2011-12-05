unit Graph;

interface
uses VertexList, EdgeList, Vertex, Edge, SysUtils;

type

TGraph = class
private
    fVertexList: TVertexList;
    fEdgeList: TEdgeList;
public
    procedure AddVertex(name: string);
    procedure DeleteVertex(name: string);
    function FindVertex(name: string; var vertex: TVertex): integer;
    procedure AddEdge(v1, v2: string; weight: integer);
    procedure DeleteEdge(v1, v2: string);
    function FindEdge(v1, v2: string; var edge: TEdge): integer;
    procedure Print;

    constructor Create;
end;


implementation

constructor TGraph.Create;
begin
  fVertexList := TVertexList.Create;
  fEdgeList := TEdgeList.Create;
end;

function TGraph.FindVertex(name: string; var vertex: TVertex): integer;
begin
  Result := fVertexList.Find(name, vertex);
end;

function TGraph.FindEdge(v1, v2: string; var edge: TEdge): integer;
var found1, found2: integer;
vertex1, vertex2: TVertex;
begin
  found1 := fVertexList.Find(v1, vertex1);
  found2 := fVertexList.Find(v2, vertex2);
  if((found1 = -1) or (found2 = -1)) then
    Result := -1
  else
    Result := fEdgeList.Find(vertex1.Id, vertex2.Id, edge);
end;

procedure TGraph.AddVertex(name: string);
begin;
    fVertexList.Add(name);
end;

procedure TGraph.DeleteVertex(name: string);
begin
    fVertexList.Delete(name);
end;

procedure TGraph.AddEdge(v1, v2: string; weight: integer);
var found1, found2: integer;
vertex, vertex2: TVertex;
begin
    found1 := fVertexList.Find(v1, vertex);
    found2 := fVertexList.Find(v2, vertex2);
    if( (found1 = -1) or (found2 = -1) ) then exit;
    fEdgeList.AddEdge(vertex.id, vertex2.id, weight);
end;

procedure TGraph.DeleteEdge(v1, v2: string);
var found1, found2: integer;
vertex, vertex2: TVertex;
begin
    found1 := fVertexList.Find(v1, vertex);
    found2 := fVertexList.Find(v2, vertex2);
    if( (found1 = -1) or (found2 = -1) ) then exit;
    fEdgeList.DeleteEdge(vertex.Id, vertex2.Id);
end;


procedure TGraph.Print;
var i,j, found: integer;
edge: TEdge;
begin
    WriteLn('Edges: ');
    for i := 0 to fVertexList.Count - 1 do
    begin
        Write(IntToStr(fVertexList[i].id) + ' ' + fVertexList[i].name + ': ');
        for j := 0 to fVertexList.Count - 1 do
        begin
            found := fEdgeList.Find(fVertexList[i].id, fVertexList[j].id, edge);
            if(found <> -1) then
                Write(IntToStr(fVertexList[j].id) + '(' + IntToStr(edge.weight) + ')');
        end;
        Writeln;
    end;
end;

end.
