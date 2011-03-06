unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, Unit2, StdCtrls;

type
  TForm1 = class(TForm)
    Button2: TButton;
    Memo1: TMemo;
    GroupBox1: TGroupBox;
    Label1: TLabel;
    Edit1: TEdit;
    Button1: TButton;
    GroupBox2: TGroupBox;
    Label4: TLabel;
    Edit2: TEdit;
    Button3: TButton;
    Button4: TButton;
    Button5: TButton;
    Button6: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
  private
  public
    { Public declarations }
  end;

  TMyStack = class(TStack)
    procedure createRandomStack;
    procedure Transponite;
  end;

var
  Form1: TForm1;
  stack : TMyStack;

implementation

procedure TMyStack.createRandomStack;
var i : integer;
begin
  top := nil;
  randomize;
  for i := 1 to 9 do
    Adds(random(100));
end;

procedure TMyStack.Transponite;
var i,j, len : integer;
temp : PElem;
b : TMyStack;
begin
  b := TMyStack.Create;

  while(top <> nil) do
  begin
    reads(j);
    b.Adds(j);
  end;
  inc(j);
  new(top);
  top^.value := b.top^.value;
  b.Reads(j);
  temp := top;
  while(b.top <> nil) do
  begin
    b.Reads(j);
    addAfter(temp, j);
    temp^.next^.next := nil;
    temp := temp^.next;
  end;
  b.Free;

end;

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
begin
  stack.Adds(strToInt(edit1.Text));
  stack.print(memo1);
  edit1.Text := '';
end;

procedure TForm1.Button2Click(Sender: TObject);
var j : integer;
begin
  stack.Reads(j);
  stack.print(memo1);
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  stack := TMyStack.Create;
end;

procedure TForm1.Button3Click(Sender: TObject);
var j : integer;
begin
  stack.poisk(strToInt(edit2.Text),j);
  if(j = -1) then
    memo1.Lines.add('Не найдено')
  else
    memo1.Lines.add('Элемент ' + intToStr(j) + ' есть в стеке');
  edit2.Text := '';
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
  stack.createRandomStack;
  stack.print(memo1);
end;

procedure TForm1.Button5Click(Sender: TObject);
begin
  stack.Transponite;
  stack.print(memo1);
end;

procedure TForm1.Button6Click(Sender: TObject);
begin
  stack.sort;
  stack.print(memo1);
end;

end.
