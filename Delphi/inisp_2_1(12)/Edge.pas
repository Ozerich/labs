unit Edge;

interface

type

TEdge = class
private
    fV1, fV2: integer;
    fWeight: integer;
public
    constructor Create(v1, v2, weight: integer);
    property Weight: integer read fWeight write fWeight;
    property V1: integer read fV1 write fV1;
    property V2: integer read fV2 write fV2;
end;

implementation

constructor TEdge.Create(v1, v2, weight: integer);
begin
    fV1 := v1;
    fV2 := v2;
    fWeight := weight;
end;

end.



