unit Polynome;

interface

uses List, Monome;

type

TOnAddMonome = procedure(coefficent: extended; varPower: integer);

TPolynome = class
  private
    fMonomeList: TList;

    fOnAddMonome: TOnAddMonome;
  public
    constructor Create;

    procedure AddMonome(coefficent: extended; varPower: integer);
    function ToString: string;
    function Calculate(variable: extended): extended;
    function GetCoefficent(varPower: integer): extended;
    function GetPower: integer;

    class function Add(a, b: TPolynome): TPolynome;
    class function Subtract(a, b: TPolynome): TPolynome;
    class function Multiply(a, b: TPolynome): TPolynome;
    class function Invert(a: TPolynome): TPolynome;

    property OnAddMonome: TOnAddMonome read fOnAddMonome write fOnAddMonome;
end;

implementation

constructor TPolynome.Create;
begin
  fMonomeList := TList.Create;
end;

procedure TPolynome.AddMonome(coefficent: extended; varPower: integer);
var
  monome: TMonome;
begin
  monome := TMonome.Create(coefficent, varPower);
  fMonomeList.AddItem(monome);
  if(Assigned(fOnAddMonome)) then
    fOnAddMonome(coefficent, varPower);
end;

function TPolynome.ToString: string;
var
  monome: TMonome;
begin
  Result := '';
  monome := fMonomeList.Process;
  while(monome <> nil) do
  begin
    if(monome.ToString = '') then
      continue;
    if(Result <> '') then
        Result := Result + ' ';
    Result := Result + monome.ToString;
    monome := fMonomeList.Process
  end;
  if(Result = '') then
    Result := '0'
  else
    Result := Copy(Result, 3, Length(Result));
end;

function TPolynome.Calculate(variable: extended): extended;
var
  monome: TMonome;
begin
  Result := 0.0;
  monome := fMonomeList.Process;
  while(monome <> nil) do
  begin
    Result := Result + monome.Calculate(variable);
    monome := fMonomeList.Process;
  end;
end;

function TPolynome.GetCoefficent(varPower: integer): extended;
var
  monome: TMonome;
begin
  monome := fMonomeList.Find(varPower);
  if(monome <> nil) then
    Result := monome.Coefficent
  else
    Result := 0.0;
end;

function TPolynome.GetPower: integer;
var
  monome: TMonome;
begin
  if(fMonomeList.Process = nil) then
    Result := -1
  else begin
    monome := fMonomeList.Process;
    while(monome <> nil) do
      monome := fMonomeList.Process;
    Result := monome.VarPower;
  end;
end;

class function TPolynome.Invert(a: TPolynome): TPolynome;
var
  monome: TMonome;
begin
  Result := TPolynome.Create;
  monome := a.fMonomeList.Process;
  while(monome <> nil) do
  begin
    Result.AddMonome(-monome.Coefficent, monome.VarPower);
    monome := a.fMonomeList.Process;
  end;
end;

class function TPolynome.Add(a, b: TPolynome): TPolynome;
var
  monomeA, monomeB: TMonome;
begin
  Result := TPolynome.Create;
  monomeA := a.fMonomeList.Process;
  while(monomeA <> nil) do
  begin
    Result.AddMonome(b.GetCoefficent(monomeA.VarPower) + monomeA.Coefficent, monomeA.VarPower);
    monomeA := a.fMonomeList.Process;
  end;

  monomeB := b.fMonomeList.Process;
  while(monomeB <> nil) do
  begin
    Result.AddMonome(a.GetCoefficent(monomeB.VarPower) + monomeB.Coefficent, monomeB.VarPower);
    monomeB := b.fMonomeList.Process;
  end;
end;

class function TPolynome.Subtract(a, b: TPolynome): TPolynome;
begin
  Result := TPolynome.Add(a, TPolynome.Invert(b));
end;

class function TPolynome.Multiply(a, b: TPolynome): TPolynome;
var
  powerA, powerB: integer;
  k, i: integer;
  coef: extended;
begin
  Result := TPolynome.Create;

  powerA := a.GetPower;
  powerB := b.GetPower;
  if((powerA = -1) or (powerB = -1)) then
  begin
    Result := nil;
    exit;
  end;

  for k := 0 to powerA + powerB - 1 do
  begin
    coef := 0.0;
    for i := 0 to k do
      coef := coef + a.GetCoefficent(i) * b.GetCoefficent(k - i);
    Result.AddMonome(coef, k);
  end;

  Result.AddMonome(a.GetCoefficent(powerA) * b.GetCoefficent(powerB), powerA + powerB);

end;




end.
