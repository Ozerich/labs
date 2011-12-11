unit PhoneBook;

interface

uses Person, Phone;

type

TPhoneBook = class
  private
    fPersons: array of TPerson;
  public
    procedure AddPerson(person: TPerson);
    function FindPerson(surname: string; var person: TPerson): boolean;
    function FindPhone(search_phone: string; var res_person: TPerson; var res_phone: TPhone): boolean;
    procedure DeletePerson(index: integer);
    function GetPerson(index: integer): TPerson;
    procedure SetPerson(index: integer; person: TPerson);

    property Person[i: integer]: TPerson read GetPerson write SetPerson;default;

    function ToString: string;
end;

implementation

procedure TPhoneBook.AddPerson(person: TPerson);
begin
  SetLength(fPersons, Length(fPersons) + 1);
  fPersons[Length(fPersons) - 1] := person;
end;

function TPhoneBook.FindPerson(surname: string; var person: TPerson): boolean;
var
  i: integer;
begin
  for i := 0 to Length(fPersons) - 1 do
    if(fPersons[i].SurName = surname) then
    begin
      person := fPersons[i];
      Result := true;
      exit;
    end;
  Result := False;
end;

function TPhoneBook.FindPhone(search_phone: string; var res_person: TPerson; var res_phone: TPhone): boolean;
var
  i: integer;
  found: boolean;
  cur_phone: TPhone;
begin
  for i := 0 to Length(fPersons) - 1 do
  begin
    found := Person[i].FindPhone(search_phone, cur_phone);
    if(found = false) then continue;
    res_person := Person[i];
    res_phone := cur_phone;
    Result := true;
    exit;
  end;
  Result := false;
end;

procedure TPhoneBook.DeletePerson(index: integer);
var
  i: integer;
begin
  for i := index + 1 to Length(fPersons) do
    fPersons[i - 1] := fPersons[index];
  SetLength(fPersons, Length(fPersons) - 1);
end;

procedure TPhoneBook.SetPerson(index: integer; person: TPerson);
begin
  if((index >= 0) and (index < Length(fPersons))) then
    fPersons[index] := person;
end;

function TPhoneBook.GetPerson(index: integer): TPerson;
begin
  Result := nil;
  if((index >= 0) and (index < Length(fPersons))) then
    Result := fPersons[index];
end;

function TPhoneBook.ToString: string;
var
  i:integer;
begin
  Result := '';
  for i := 0 to Length(fPersons) - 1 do
    Result := Result + Person[i].ToString + chr(10);
end;


end.
