unit EdgeList;

interface
uses Edge, Vertex;

type

TEdgeList = class
private
    fEdges: array of TEdge;
    fExistEdge: array [0..255, 0..255] of boolean;
public
    procedure AddEdge(v1, v2, weight: integer);
    procedure DeleteEdge(v1, v2: integer);
    function Find(v1, v2: integer; var edge: TEdge): integer;
    function ExistEdge(v1, v2: integer): boolean;

    constructor Create;
end;

implementation

constructor TEdgeList.Create;
begin
  SetLength(fEdges, 0);
end;

function TEdgeList.ExistEdge(v1, v2: integer): boolean;
begin
    Result := fExistEdge[v1, v2];
end;

procedure TEdgeList.AddEdge(v1, v2, weight: integer);
var
    Edge: TEdge;
begin
    Edge := TEdge.Create(v1, v2, weight);
    SetLength(fEdges, Length(fEdges) + 1);
    fEdges[Length(fEdges) - 1] := Edge;
    fExistEdge[v1, v2] := true;
    fExistEdge[v2, v1] := true;
end;

function TEdgeList.Find(v1, v2: integer; var edge: TEdge): integer;
var
    i: integer;
    found1, found2: integer;
    vertex1, vertex2: TVertex;
begin
    Result := -1;
    for i := 0 to Length(fEdges) - 1 do
        if( ((fEdges[i].v1 = v1) and (fEdges[i].v2 = v2)) or ((fEdges[i].v2 = v1) and (fEdges[i].v1 = v2)) ) then
        begin
            edge := fEdges[i];
            Result := i;
            exit;
        end;
end;

procedure TEdgeList.DeleteEdge(v1, v2: integer);
var i, found: integer;
edge: TEdge;
begin
    if(fExistEdge[v1, v2] = false) then exit;
    fExistEdge[v1, v2] := false;
    fExistEdge[v2, v1] := false;
    found := Find(v1, v2, edge);
    for i := found to Length(fEdges) - 2 do
        fEdges[i] := fEdges[i + 1];
    SetLength(fEdges, Length(fEdges) - 1);
end;




end.
