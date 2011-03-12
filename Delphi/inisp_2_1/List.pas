unit List;

interface
uses Monome, ListElem;

type

TList = class
  private
    fBeg, fStep: TListElem;
  public
    constructor Create;

    procedure AddItem(value: TMonome);
    function Process: TMonome;
    function Find(power: integer): TMonome;
    
end;

implementation

constructor TList.Create;
begin
  fBeg := TListElem.Create;
  fStep := TListElem.Create;
end;



procedure TList.AddItem(value: TMonome);
var
  newElem, temp, last, temp2: TListElem;
begin
  newElem := TListElem.Create(value);
  last := nil;
  if(fBeg = nil) then
    fBeg := newElem
  else if(value.VarPower < fBeg.value.VarPower) then
  begin
    newElem.next := fBeg;
    fBeg := newElem;
  end else
  begin
    temp := fBeg;
    while(temp <> nil) do
    begin
      last := temp;

      if(temp.value.VarPower = value.VarPower) then
      begin
        temp.value.Coefficent := value.Coefficent;
        exit;
        break;
      end;

      if(temp.next = nil) then
      begin
        temp.next := newElem;
        break;
      end;

      if((temp.next.value.VarPower > value.VarPower) and (temp.value.VarPower < value.VarPower)) then
      begin
        temp2 := temp.next;
        temp.next := newElem;
        newElem.next := temp2;
        break;
      end;
      temp := temp.next;
    end;

    if(temp = nil) then
      last.next := newElem;
  end;
    
  fStep := fBeg;
end;

function TList.Process: TMonome;
begin
  if(fStep = nil) then
  begin
    fStep := fBeg;
    Result := nil;
  end else
  begin
    Result := fStep.value;
    fStep := fStep.next;
  end;
end;

function TList.Find(power: integer): TMonome;
var temp: TListElem;
begin
  temp := fBeg;
  while(temp <> nil) do
  begin
    if(temp.value.VarPower = power) then
    begin
      Result := temp.value;
      exit;
    end;
    temp := temp.next;
  end;
  Result := nil;
end;

end.
