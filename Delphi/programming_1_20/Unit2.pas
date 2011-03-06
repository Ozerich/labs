unit Unit2;

interface
Uses
  Grids, SysUtils;

type
  PRec = ^TRec;
  TRec = record
    Col: longint;
    inf: extended;
    a: PRec;
  end;

  TList = class
  public
    Root: PRec;
    constructor Create();
    destructor Destroy();
    procedure Add(x: extended; After: PRec; Col: longint);
    procedure Delete(x, prev: PRec);
    procedure Clear();
    function average:extended;
  end;

  TMatrix = class
  private
    h: array of TList;
    Rows, Cols: longint;
  public
    constructor Create(RowCount, ColCount: longint);
    destructor Destroy();
    procedure Add(x, y: longint; Value: extended);
    procedure Display(StringGrid: TStringGrid);
    procedure Delete(x, y: longint);
    function getAverageValue:extended;
    procedure fillMatrix(value:extended; var matrix_less:TMatrix; var matrix_more:TMatrix);
  end;

implementation

{ TList }

procedure TList.Add(x: extended; After: PRec; Col: longint);
var
  rec: PRec;
begin
  new(rec);
  rec.inf := x;
  rec.Col := Col;
  if (After = nil)
    then
      begin
        rec.a := Root;
        Root := rec;
      end
    else
      begin
        rec.a := After.a;
        After.a := rec;
      end;
end;

function TList.average:extended;
var
  rec,t:PRec;
  sum:extended;
  count:integer;
begin
  rec := Root;
  sum := 0.0;
  count := 0;
  while(rec <> nil) do
  begin
    inc(count);
    sum := sum + rec^.inf;
    rec := rec^.a;
  end;
  if (count > 0) then
    Result := sum / count
  else
    Result := 0.0;
end;

procedure TList.Clear;
var
  rec, t: PRec;
begin
  rec := Root;
  while (rec <> nil) do
    begin
      t := rec.a;
      dispose(rec);
      rec := t;
    end;       
end;

constructor TList.Create;
begin
  inherited;
  Root := nil;
end;

procedure TList.Delete(x, prev: PRec);
begin
  if (x = nil)
    then exit;

  if (prev <> nil)
    then prev.a := x.a;

  if (x = Root)
    then Root := x.a;

  dispose(x);
end;

destructor TList.Destroy;
begin
  Clear;
  inherited;
end;

{ TMatrix }

procedure TMatrix.Add(x, y: Integer; Value: extended);
var
  rec: PRec;
begin
  if (Value = 0) then exit;

  rec := h[x].Root;
  while (rec <> nil) and (rec.a <> nil) and (rec.a.Col <= y) do
    rec := rec.a;

  if (rec <> nil)
  and(rec.Col = y)
    then rec.inf := Value
    else h[x].Add(Value, rec, y);
end;

constructor TMatrix.Create(RowCount, ColCount: Integer);
var
  i: longint;
begin
  inherited Create;
  Rows := RowCount;
  Cols := ColCount;

  SetLength(h, RowCount);
  for i := 0 to RowCount - 1 do
    h[i] := TList.Create;
end;

procedure TMatrix.Delete(x, y: Integer);
var
  rec, prev: PRec;
begin
  prev := nil;
  rec := h[x].Root;
  while (rec <> nil) and (rec.Col < y) do
    begin
      prev := rec;
      rec := rec.a;
    end;

  if (rec = nil)
  or (rec.Col <> y)
    then exit;

  h[x].Delete(rec, prev);
end;

destructor TMatrix.Destroy;
var
  i: longint;
begin
  for i := 0 to Rows - 1 do
    h[i].Destroy;

  SetLength(h, 0);
end;

procedure TMatrix.Display(StringGrid: TStringGrid);
var
  i, j: longint;
  rec: PRec;
  t: string;
begin
  for i := 0 to StringGrid.RowCount - 1 do
    for j := 0 to StringGrid.ColCount - 1 do
      StringGrid.Cells[j, i] := '0';
  for i := 0 to Rows - 1 do
    begin
      rec := h[i].Root;
      while (rec <> nil) do
        begin
          t := FloatToStr(rec.inf);
          StringGrid.Cells[rec.Col, i] := t;
          rec := rec.a;
        end;
    end;
end;

function TMatrix.getAverageValue:extended;
var sum,t:extended;
i,count:integer;
begin
  sum := 0.0;
  count := 0;
  for i:=0 to Rows - 1 do
  begin
    t := h[i].average;
    if(t <> 0.0) then
    begin
      inc(count);
      sum := sum + t;
    end;
  end;
  if(count > 0)then
    Result := sum / count
  else
    Result := 0.0;
end;

procedure TMatrix.fillMatrix(value:extended; var matrix_less:TMatrix; var matrix_more:TMatrix);
var i:integer;
cur : PRec;
begin
  for i := 0 to Rows - 1 do
  begin
    cur := h[i].Root;
    while(cur <> nil) do
    begin
      if(cur^.inf < value) then
        matrix_less.Add(i, cur^.Col, cur^.inf)
      else if(cur^.inf > value) then
        matrix_more.Add(i, cur^.Col, cur^.inf);
      cur := cur^.a;
    end;
  end;

end;

end.
