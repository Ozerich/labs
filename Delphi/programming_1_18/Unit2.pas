unit unit2;

interface
uses   StdCtrls,ComCtrls,SysUtils;

type

    TItem = record
        name : string[80];
        passport : integer;
    end;
TMs = array[1..100] of TItem;

    PNode = ^TNode;
    TNode = record 
        value : TItem;
        left, right : PNode;
    end;
    
    TTree = class(TObject)
        root : PNode;
        constructor Create;
        procedure Add(value : TItem);
        procedure PrintToTreeView(treeView : TTreeView);
        procedure Walk(memo : TMemo);
        procedure Balans(items:TMs; n:integer);
        function Find(key:integer; var res:TItem):integer;
        procedure clear; 
    end;

implementation

constructor TTree.Create;
begin
    inherited Create;
    clear;
end;

procedure TTree.clear;
begin
    root := nil;
end;

Procedure TTree.Balans(items:TMs; n:integer);

	Procedure BL(i,j:Word);
	  Var m:Word;
	  begin
		if i<=j then
		  begin
			m:=(i+j) div 2;
			Add(items[m]);
			BL(i,m-1);
			BL(m+1,j);
		  end;
	  end;


begin
  root := nil;
  BL(1,n);
end;


function TTree.Find(key:integer; var res:TItem):integer;
  function find_rec(cur:PNode; key:integer):PNode;
  begin
    if(cur = nil)then
      Result := nil
    else begin
      if(cur^.value.passport = key)then
        Result := cur
      else if(cur^.left <> nil) and (key < cur^.value.passport)then
        Result := find_rec(cur^.left, key)
      else if(cur^.right <> nil) and (key > cur^.value.passport)then
        Result := find_rec(cur^.right, key)
      else
        Result := nil;
    end;
  end;

  var find_res : PNode;
begin
    find_res := find_rec(root, key);
    if(find_res = nil)then Result := 0
    else begin
      Result := 1;
      res := find_res^.value;
    end;
end;





procedure TTree.Add(value : TItem);
var
    temp : PNode;   
    procedure add_node(cur, item : PNode);
    begin
        if(item^.value.passport > cur^.value.passport) then
        begin
            if(cur^.right = nil) then
            begin
                new(temp);
                temp^.value := item^.value;
                temp^.left := nil;
                temp^.right := nil;
                cur^.right := temp;
            end
            else
                add_node(cur^.right, item)
        end else
        begin
            if(cur^.left = nil) then
            begin
                new(temp);
                temp^.value := item^.value;
                temp^.left := nil;
                temp^.right := nil;
                cur^.left := temp;
            end
            else
                add_node(cur^.left, item)
        end; 
    end;
begin
    new(temp);
    temp^.right := nil;
    temp^.left := nil;
    temp^.value := value;
    if(root = nil) then
        root := temp
    else
        add_node(root, temp);
end;

procedure TTree.Walk(memo : Tmemo);
  procedure print_node(node : PNode);
  begin
    memo.Lines.Add(node^.value.name);
    if(node^.left <> nil) then
      print_node(node^.left);
    if(node^.right <> nil) then
      print_node(node^.right);
  end;
begin
  memo.Clear;
  print_node(root);
end;

procedure TTree.PrintToTreeView(treeView : TTreeView);
  function count(node : PNode) : integer;
  begin
    result := 0;
    if(node = nil)then exit;
    inc(result, count(node^.left));
    inc(result, count(node^.right));
    inc(result);
  end;
  procedure print_node(parent_node : TTreeNode; node : PNode);
  var cur_node : TTreeNode;
  begin
    if(node = nil) then exit;
    cur_node := treeView.Items.AddChild(parent_node, node^.value.name + ' - ' + intToStr(node^.value.passport) + '(' +
      intToStr(count(node^.left)) +')');
    print_node(cur_node, node^.left);
    print_node(cur_node, node^.right);
  end;
begin
  treeView.Items.Clear;
  print_node(nil, root);
end;


end.

