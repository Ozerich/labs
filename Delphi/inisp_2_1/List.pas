unit List;

interface
uses Monome;

type

TElem = class
  public
    value: TMonome;
    next: TElem;
end;

TList = class
  private
    fBeg, fStep: TElem;
  public
    constructor Create;

    procedure AddItem(value: TMonome);
    function Process: TMonome;
    function Find(power: integer): TMonome;
    
end;

implementation

constructor TList.Create;
begin
  fBeg := nil;
  fStep := nil;
end;



procedure TList.AddItem(value: TMonome);
var
  newElem, temp, last, temp2: TElem;
begin
  newElem := TElem.Create;
  last := nil;
  newElem.value := value;
  if(fBeg = nil) then
    fBeg := newElem
  else if(value.GetVarPower < fBeg.value.GetVarPower) then
  begin
    newElem.next := fBeg;
    fBeg := newElem;
  end else
  begin
    temp := fBeg;
    while(temp <> nil) do
    begin
      last := temp;

      if(temp.value.GetVarPower = value.GetVarPower) then
      begin
        temp.value.SetCoefficent(value.GetCoefficent);
        exit;
        break;
      end;

      if(temp.next = nil) then
      begin
        temp.next := newElem;
        break;
      end;

      if((temp.next.value.GetVarPower > value.GetVarPower) and (temp.value.GetVarPower < value.GetVarPower)) then
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
var temp: TElem;
begin
  temp := fBeg;
  while(temp <> nil) do
  begin
    if(temp.value.GetVarPower = power) then
    begin
      Result := temp.value;
      exit;
    end;
    temp := temp.next;
  end;
  Result := nil;
end;



end.
