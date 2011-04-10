unit Person;

interface

uses Phone;

type

TPerson = class
  private
    fName: string;
    fSurName: string;
    fPhones: array of TPhone;
  public
    constructor Create(per_name: string; per_surname: string);
    procedure AddPhone(phone: TPhone);
    function FindPhone(phone_number: string; var Phone: TPhone): boolean;
    procedure DeletePhone(index: integer);

    procedure SetPhone(index: integer; phone: TPhone);
    function GetPhone(index: integer): TPhone;

    property Name:string read fName write fName;
    property SurName:string read fSurName write fSurName;
    property Phone[i: integer]: TPhone read GetPhone write SetPhone;default;

    function ToString: string;
end;

implementation

constructor TPerson.Create(per_name: string; per_surname: string);
begin
  Name := per_name;
  SurName := per_surname;
end;

procedure TPerson.AddPhone(phone: TPhone);
begin
  SetLength(fPhones, Length(fPhones) + 1);
  fPhones[Length(fPhones) - 1] := phone;
end;

function TPerson.FindPhone(phone_number: string; var Phone: TPhone): boolean;
var i: integer;
begin
  for i := 0 to Length(fPhones) do
    if(fPhones[i].Number = phone_number) then
    begin
      Result := True;
      Phone := fPhones[i];
      exit;
    end;
  Result := False;
end;

procedure TPerson.DeletePhone(index: integer);
var i: integer;
begin
  for i := index + 1 to Length(fPhones) do
    fPhones[i - 1] := fPhones[index];
  SetLength(fPhones, Length(fPhones) - 1);
end;

procedure TPerson.SetPhone(index: integer; phone: TPhone);
begin
  if((index >= 0) and (index < Length(fPhones))) then
    fPhones[index] := phone;
end;

function TPerson.GetPhone(index: integer): TPhone;
begin
  Result := nil;
  if((index >= 0) and (index < Length(fPhones))) then
    Result := fPhones[index];
end;

function TPerson.ToString: string;
var
  i: integer;
begin
  Result := 'Name: ' + name + chr(10);
  Result := Result + 'Surname: ' + surname + chr(10);
  Result := Result + 'Phones:' + chr(10);
  for i := 0 to Length(fPhones) - 1 do
    Result := Result + Phone[i].PhoneNumber + chr(10);
end;

end.
