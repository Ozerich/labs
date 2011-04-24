unit VertexList;

interface

uses Vertex;

type

TVertexList = class
private
    fVertexes: array of TVertex;
public
    procedure Add(name: string);
    procedure Delete(name: string);
    function Find(name: string; var vertex: TVertex): integer;
    function GetCount: integer;
    function GetVertex(ind: integer): TVertex;

    constructor Create;

    property Count: integer read GetCount;
    property Vertex[ind: integer]: TVertex read GetVertex;default;
end;


implementation

Constructor TVertexList.Create;
begin
  SetLength(fVertexes, 0);
end;

function TVertexList.GetVertex(ind: integer): TVertex;
begin
    Result := fVertexes[ind];
end;

function TVertexList.GetCount: integer;
begin
    Result := Length(fVertexes);
end;

procedure TVertexList.Add(name: string);
var
    vertex: TVertex;
begin
    vertex := TVertex.Create(Length(fVertexes) + 1, name);
    SetLength(fVertexes, Length(fVertexes) + 1);
    fVertexes[Length(fVertexes) - 1] := vertex;
end;

procedure TVertexList.Delete(name: string);
var
    vertex: TVertex;
    found, i: integer;
begin
    found := Find(name, vertex);
    if(found = -1) then exit;
    for i := found to Length(fVertexes) - 2 do
        fVertexes[i] := fVertexes[i + 1];
    SetLength(fVertexes, Length(fVertexes) - 1);
end;

function TVertexList.Find(name: string; var vertex: TVertex): integer;
var
    i: integer;
begin
    Result := -1;
    for i := 0 to Length(fVertexes) - 1 do
        if(fVertexes[i].name = name) then
        begin
            vertex := fVertexes[i];
            Result := i;
            exit;
        end;
end;


end.
