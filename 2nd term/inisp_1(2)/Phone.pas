unit Phone;

interface



const
  TYPE_HOME = 1;
  TYPE_MOBILE = 2;
  TYPE_WORK = 3;

type

TPhoneType = (TypeHome = 1, TypeWork = 2, TypeMobile = 3);
TPhone = class
  private
    fCityCode: string;
    fCountryCode: string;
    fNumber: string;
    fPhoneType: TPhoneType;

    function GetPhoneNumber: string;
    procedure SetPhoneNumber(str: string);


  public
    constructor Create(phone: string);overload;
    constructor Create(_cityCode: string; _countryCode:string; _number: string);overload;

    property PhoneNumber:string read GetPhoneNumber write SetPhoneNumber;
    property CityCode: string read fCityCode write fCityCode;
    property CountryCode: string read fCountryCode write fCountryCode;
    property Number: string read fNumber write fNumber;
    property PhoneType: TPhoneType read fPhoneType write fPhoneType;
end;

implementation

constructor TPhone.Create(phone: string);
begin
  SetPhoneNumber(phone);
end;

constructor TPhone.Create(_cityCode: string; _countryCode: string; _number: string);
begin
  CityCode := _cityCode;
  CountryCode := _countryCode;
  Number := _number;
end;

function TPhone.GetPhoneNumber: string;
var i: integer;
begin
  Result := '+' + '(' + CountryCode + '-' + CityCode + ')';
  for i := 1 to Length(Number) do
  begin
    Result := Result + Number[i];
    if((i > 1) and (i mod 2 = 1) and (i < Length(Number))) then
      Result := Result + '-';
  end;
end;

procedure TPhone.SetPhoneNumber(str: string);
var i, p: integer;
begin
  str := Copy(str, 3, Length(str) - 3);
  p := Pos('-', str);
  CountryCode := Copy(str, 1, p - 1);
  str := Copy(str, p + 1, Length(str) - p - 1);
  p := Pos(')', str);
  CityCode := Copy(str, 1, p - 1);
  str := Copy(str, p + 1, Length(str) - p + 1);
  Number := '';
  for i := 1 to Length(str) do
    if(str[i] <> '-') then
      Number := Number + str[i];
end;


end.
 