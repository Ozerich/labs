unit ListElem;

interface
uses Monome;

type

TListElem = class
private
    fValue: TMonome;
    fNext: TListElem;
public
    property Value: TMonome read fValue write fValue;
    property Next: TListElem read fNext write fNext;

    constructor Create;virtual;overload;
    constructor Create(value: TMonome);overload;

    destructor Destroy;override;
end;

implementation

constructor TListElem.Create;
begin
    Value := TMonome.Create;
    Next := nil;
end;

destructor TListElem.Destroy;
begin
    
end;

constructor TListElem.Create(value: TMonome);overload;
begin
    self.value := value;
    self.Next := nil;
end;

end.
