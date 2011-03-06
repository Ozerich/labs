program lab1;

{$APPTYPE CONSOLE}

uses
  Polynome, SysUtils;

const
  UNKNOWN_COMMAND = 'Unknown command';
  POL_INDEX = 'Polynom index';
  READ_POWER = 'Power';
  READ_COEF = 'New coefficent';
  READ_VARIABLE = 'Variable value';

var
  data: array of TPolynome;
  i, power, index: integer;
  cmd: string;
  coef, oldCoef, variable: extended;


function InputIndex(hint: string; var index: integer): boolean;
var
  value: integer;
begin
  write('...' + hint + ' ');
  readln(value);

  Result := true;

  if((value <= 0) or (value > Length(data))) then
  begin
    writeln('Incorrect index');
    Result := false;
  end else
    index := value;
end;

function InputInt(hint: string; var value: integer): boolean;
begin
  write('...' + hint + ' ');
  readln(value);
  Result := true;
end;


function InputFloat(hint: string; var value: extended): boolean;
begin
  write('...' + hint + ' ');
  readln(value);
  Result := true;
end;

procedure WriteResult(text: string);
begin
  writeln('Result: ', text);
  writeln('Press any key to continue...');
  Readln;
end;

procedure math_op(operation: string);
var
  index1, index2: integer;
  ans: string;
  res: TPolynome;
begin
  res := TPolynome.Create;

  if(InputIndex('First argument index', index1) = false) then exit;
  if(InputIndex('Second argument index', index2) = false) then exit;

  if(operation = 'ADD') then
    res := TPolynome.Add(data[index1 - 1], data[index2 - 1])
  else if(operation = 'SUB') then
    res := TPolynome.Subtract(data[index1 - 1], data[index2 - 1])
  else if(operation = 'MUL') then
    res := TPolynome.Multiply(data[index1 - 1], data[index2 - 1]);

  Write('Variable value(type "skip" if you want see overall solution) ');
  Readln(ans);
  if(ans = 'skip') then
    WriteResult(res.ToString)
  else
    WriteResult(FloatToStr(res.Calculate(StrToFloat(ans))));
end;

begin
  writeln('Vital Ozierski, 052004 group (c) 2011');
  readln;
  while(true) do
  begin
    writeln;
    writeln('Polynomes');
    for i := 0 to Length(data) - 1 do
      writeln(IntToStr(i + 1) + ': ' + data[i].ToString);
    writeln;
    writeln('Commands: add, edit, calc, math_add, math_sub, math_mul, exit');
    write('> ');

    readln(cmd);
    if(cmd = 'add') then
    begin
      SetLength(data, Length(data) + 1);
      data[Length(data) - 1] := TPolynome.Create;
      writeln('The polynome is added. You can edit it');
    end
    else if(cmd = 'edit') then
    begin
      if(InputIndex(POL_INDEX, index) = false) then continue;
      if(InputInt(READ_POWER, power) = false) then continue;
      oldCoef := data[index - 1].GetCoefficent(power);
      writeln('Old coefficent: ' + FloatToStr(oldCoef));
      if(InputFloat(READ_COEF, coef) = false) then continue;
      data[index - 1].AddMonome(coef, power);
    end
    else if(cmd = 'calc') then
    begin
      if(InputIndex(POL_INDEX, index) = false) then continue;
      if(InputFloat(READ_VARIABLE, variable) = false) then continue;
      WriteResult(FloatToStr(data[index - 1].Calculate(variable)));
    end
    else if(cmd = 'math_add') then
      math_op('ADD')
    else if(cmd = 'math_sub') then
      math_op('SUB')
    else if(cmd = 'math_mul') then
      math_op('MUL')
    else if(cmd = 'exit') then
      break
    else
      writeln(UNKNOWN_COMMAND);
  end;
end.
