unit Application;

interface
  uses SysUtils, PhoneBook, Person, Phone;

type

TApplication = class
  private
    PhoneBook: TPhoneBook;

    procedure AddPerson;
    procedure EditPerson;
    procedure Search;
    procedure DeletePerson;

  public
    constructor Create;
    procedure Run;
end;

implementation

function InputString(text: string): string;
begin
  write(text);
  readln(Result);
end;

function InputInt(text: string): integer;
begin
  write(text);
  readln(Result);
end;


constructor TApplication.Create;
begin
  PhoneBook := TPhoneBook.Create;
  WriteLn('Lab N1, Yuri Krylov, 052001');
  readln;
end;

procedure TApplication.DeletePerson;
var
  index: integer;
begin
  index := InputInt('Person index: ');
  PhoneBook.DeletePerson(index);
end;


procedure TApplication.AddPerson;
var
  person: TPerson;
  name, surname: string;
begin
  name := InputString('Person name: ');
  surname := InputString('Person surname: ');
  person := TPerson.Create(name, surname);
  PhoneBook.AddPerson(person);
  writeln('Person is added');
  readln;
end;

procedure TApplication.EditPerson;
var
  index, index2: integer;
  person: TPerson;
  phone: Tphone;
  cmd, cmd2, cmd3, str: string;
begin
  index := InputInt('Person index: ');
  person := PhoneBook[index - 1];
  if(person = nil) then
  begin
    writeln('Incorrect index');
    exit;
  end;
  writeln(person.ToString);
  writeln('Commands: edit_name, edit_surname, edit_phones');
  write('> ');
  readln(cmd);
  if(cmd = 'edit_name') then
  begin
    Writeln('Old name: ', person.Name);
    Person.Name := InputString('New name: ');
    Writeln('Name is changed');
  end else
  if(cmd = 'edit_surname') then
  begin
    Writeln('Old surname: ', person.Surname);
    Person.SurName := InputString('New surname: ');
    Writeln('Surname is changed');
  end else
  if(cmd = 'edit_phones') then
  begin
    Writeln('Commands: add_phone, edit_phone, delete_phone');
    Write('> ');
    Readln(cmd2);
    if(cmd2 = 'add_phone') then
    begin
      str := InputString('Phone number +(xx-xxx)xxx-xx-xx : ');
      phone := TPhone.Create(str);
      person.AddPhone(phone);
      writeln('Phone added');
    end else
    if(cmd2 = 'edit_phone') then
    begin
      index2 := InputInt('Phone index: ');
      phone := Person[index2 - 1];
      if(phone = nil) then
        writeln('Incorrect index')
      else begin
        writeln('Commands: edit_countrycode, edit_citycode, edit_number, input_new');
        writeln('> ');
        readln(cmd3);
        if(cmd3 = 'edit_countrycode') then
        begin
          writeln('Old country code: ' + phone.CountryCode);
          phone.CountryCode := InputString('New country code: ');
        end else if(cmd3 = 'edit_citycode') then
        begin
          writeln('Old country code: ' + phone.CityCode);
          phone.CityCode := InputString('New city code: ');
        end else if(cmd3 = 'edit_number') then
        begin
          writeln('Old number: ' + phone.Number);
          phone.Number := InputString('New number: ');
        end else if(cmd3 = 'input_new') then
        begin
          phone.PhoneNumber := InputString('New phone: ');
        end else writeln('Incorrect command');
      end;
      Person[index2 - 1] := phone;
      writeln('Phone updated');
    end else
    if(cmd2 = 'delete_phone') then
    begin
      index := InputInt('Phone index: ');
      person.DeletePhone(index);
      writeln('Phone deleted');
    end else writeln('Incorrect command');
  end else writeln('Incorrect command');
  writeln('Person updated');
  PhoneBook[index - 1] := person;
  Readln;
end;

procedure TApplication.Search;
var
  cmd,str: string;
  person: TPerson;
  phone: TPhone;
  found: boolean;
begin
  WriteLn('Commands: by_surname, by_phone');
  Write('> ');
  Readln(cmd);
  if(cmd = 'by_surname') then
  begin
    str := InputString('Surname for search: ');
    found := PhoneBook.FindPerson(str, person);
    if(found) then
      writeln('Person is found:' + chr(10) + person.ToString)
    else
      writeln('Person is no found');
  end else
  if(cmd = 'by_phone') then
  begin
    str := InputString('Phone for search(only digits): ');
    found := PhoneBook.FindPhone(str, person, phone);
    if(found) then
      writeln('Phone is found: ' + chr(10) + phone.PhoneNumber + ' - ' + person.Name + ' ' + person.SurName)
    else
      writeln('Phone is no found');
  end else writeln('Incorrect command');

end;


procedure TApplication.Run;
var str: string;
begin
  while(true)do
  begin
    Writeln;
    Writeln('Commands: add, edit, delete, view, search, exit');
    Writeln('> ');
    Readln(str);
    if(str = 'add') then AddPerson
    else if(str = 'edit') then EditPerson
    else if(str = 'view') then WriteLn(PhoneBook.ToString)
    else if(str = 'search') then Search
    else if(str = 'exit') then break
    else if(str = 'delete') then DeletePerson
    else Writeln('Incorrect input');
  end;
end;

end.
