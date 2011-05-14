unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, Grids, Unit2;

type
  TForm1 = class(TForm)
    StringGrid1: TStringGrid;
    Panel1: TPanel;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    StringGrid2: TStringGrid;
    Label1: TLabel;
    StringGrid3: TStringGrid;
    Label2: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    Matrix, Matrix1, Matrix2: TMatrix;
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
var
  i, j, r, c, x: longint;
  s1, s2: string;
begin
  if (not InputQuery(Caption, 'Enter Rows Count', s1))
  or (not TryStrToInt(s1, r))
    then exit;
  if (not InputQuery(Caption, 'Enter Columns Count', s2))
  or (not TryStrToInt(s2, c))
    then exit;

  If (Assigned(Matrix))
    then try Matrix.Destroy; except; end;

  Matrix := TMatrix.Create(r,c);
  StringGrid1.RowCount := r;
  StringGrid1.ColCount := c;

  Randomize;
  for i := 1 to r do
    for j := 1 to c do
      begin
        if (random(100) < 85)
          then x := 0
          else x := random(100);
        Matrix.Add(i - 1, j - 1, x);
      end;

  Matrix.Display(StringGrid1);
end;

procedure TForm1.Button2Click(Sender: TObject);
var
  s1: string;
  x: extended;
begin
  if (not InputQuery(Caption, 'Enter New Value', s1))
    then exit;

  if (TryStrToFloat(s1, x))
    then Matrix.Add(StringGrid1.Row, StringGrid1.Col, x)
    else Matrix.Delete(StringGrid1.Row, StringGrid1.Col);

  Matrix.Display(StringGrid1);
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
  Matrix.Delete(StringGrid1.Row, StringGrid1.Col);
  Matrix.Display(StringGrid1);
end;

procedure TForm1.Button4Click(Sender: TObject);
var average:extended;
begin
  average:=Matrix.getAverageValue;

  Matrix1:=TMatrix.Create(StringGrid1.RowCount, StringGrid1.ColCount);
  Matrix2:=TMatrix.Create(StringGrid1.RowCount, StringGrid1.ColCount);

  StringGrid2.ColCount :=  StringGrid1.ColCount;
  StringGrid2.RowCount :=  StringGrid1.RowCount;
  StringGrid3.ColCount :=  StringGrid1.ColCount;
  StringGrid3.RowCount :=  StringGrid1.RowCount;

  Matrix.fillMatrix(average, Matrix1, Matrix2);

  Matrix1.Display(StringGrid2);
  Matrix2.Display(StringGrid3);
end;

end.
