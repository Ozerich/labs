Unit Vertex;

interface

type

TVertex = class
private
    fId: integer;
    fName: string;
public
    constructor Create(id: integer; name: string);
    property Id: integer read fId write fId;
    property Name: string read fName write fName;
end;

implementation

constructor TVertex.Create(id: integer; name: string);
begin
    fId := id;
    fName := name;
end;

end.
