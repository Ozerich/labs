unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ComCtrls, Unit2, StdCtrls, Grids;

type
  TForm1 = class(TForm)
    TreeView1: TTreeView;
    Label1: TLabel;
    Edit1: TEdit;
    Button1: TButton;
    StringGrid1: TStringGrid;
    Button2: TButton;
    Label2: TLabel;
    Memo1: TMemo;
    Button3: TButton;
    Button4: TButton;
    GroupBox1: TGroupBox;
    Label3: TLabel;
    Edit2: TEdit;
    Button6: TButton;
    Label4: TLabel;
    Label5: TLabel;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  n : integer;
  tree : TTree;

implementation

{$R *.dfm}

procedure TForm1.FormCreate(Sender: TObject);
begin
  tree := TTree.Create;
  n := 9;
  edit1.Text := intToStr(n);
  with StringGrid1 do
  begin
    Cells[0,0]:='Ф.И.О.'; 			Cells[1,0]:='Номер';
    Cells[0,1]:='Иванов А.А.'; Cells[1,1]:='100005';
    Cells[0,2]:='Петров С.И.';	 Cells[1,2]:='100002';
    Cells[0,3]:='Сидоров К.М.'; Cells[1,3]:='100004';
    Cells[0,4]:='Васильев М.К.'; Cells[1,4]:='100001';
    Cells[0,5]:='Смирнов В.К.';  Cells[1,5]:='100007';
    Cells[0,6]:='Мишин Т.В.'; Cells[1,6]:='100006';
    Cells[0,7]:='Долин А.К.';	Cells[1,7]:='100008';
    Cells[0,8]:='Катаев А.М.'; Cells[1,8]:='100000';
    Cells[0,9]:='Рубан В.В.';	Cells[1,9]:='100009';
  end;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  n := strToInt(edit1.Text);
  stringGrid1.RowCount := n + 1;
end;

procedure TForm1.Button2Click(Sender: TObject);
var i : integer;
item : TItem;
begin
  tree.clear;
  for i := 1 to n do
  begin
    item.name := stringGrid1.Cells[0,i];
    item.passport := StrToInt(stringGrid1.Cells[1,i]);
    tree.Add(item);
  end;
  tree.PrintToTreeView(treeView1);
  treeView1.FullExpand;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
  tree.Walk(memo1);
end;

procedure TForm1.Button6Click(Sender: TObject);
var item:Titem;
count:integer;
begin
  count := tree.Find(StrToInt(Edit2.Text), item);
  if(count = 0) then label5.Caption := 'Не найдено'
  else Label5.Caption := item.name;
end;

procedure TForm1.Button4Click(Sender: TObject);
var ms : TMs;
i:integer;
begin
  //GetMem(ms, sizeof(TItem)*(stringGrid1.RowCount-1));
  for i:=1 to StringGrid1.RowCount - 1 do
  begin
    ms[i].name := StringGrid1.Cells[0, i];
    ms[i].passport := StrToInt(StringGrid1.Cells[1, i]);
  end;
  tree.Balans(ms,StringGrid1.RowCount - 1);
  tree.PrintToTreeView(treeView1);
  treeView1.FullExpand;
end;

end.
