unit Monome;

interface
uses Math, SysUtils;

type

TMonome = class
  private
    fCoefficent: extended;
    fVarPower: integer;
  public
    constructor Create(coefficent: extended; varPower: integer);

    procedure SetCoefficent(value: extended);
    procedure SetVarPower(value: integer);

    function GetCoefficent: extended;
    function GetVarPower: integer;

    function Calculate(variable: extended): extended;
    function ToString: string;
end;

implementation

constructor TMonome.Create(coefficent: extended; varPower: integer);
begin
  fCoefficent := coefficent;
  fVarPower := varPower;
end;

procedure TMonome.SetCoefficent(value: extended);
begin
  fCoefficent := value;
end;

procedure TMonome.SetVarPower(value: integer);
begin
  fVarPower := value;
end;

function TMonome.GetCoefficent: extended;
begin
  Result := fCoefficent;
end;

function TMonome.GetVarPower:integer;
begin
  Result := fVarPower;
end;

function TMonome.Calculate(variable: extended): extended;
begin
  Result := GetCoefficent * power(variable, GetVarPower);
end;

function TMonome.ToString: string;
begin
  if(getCoefficent = 0.0) then
    Result := ''
  else
    Result := FloatToStr(GetCoefficent) + '*x^' + FloatToStr(GetVarPower);
  if(Length(Result) > 0) then
  begin
    if(Result[1] = '-') then
      Result := '- ' + Copy(Result, 2, Length(Result) - 1)
    else
      Result := '+ ' + Result;
  end;

end;


end.
