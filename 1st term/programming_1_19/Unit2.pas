unit Unit2;

interface
  uses StdCtrls,SysUtils;

  const HASH_SIZE:integer = 100;
type
  TItem = record
    str : string;
    key : integer;
  end;

  PNode = ^TNode;
  TNode = record
    value : TItem;
    next : PNode;
  end;

  TMs = array[1..1] of PNode;
  PMs = ^Tms;




  THashTable = class(TObject)
    m : integer;
    hash : PMs;
    constructor create(_m : integer);
    procedure clear;
    procedure add(item : TItem);
    function find(key : integer; var item : TItem) : boolean;
    procedure print(memo : TMemo);
    procedure delete(key : integer);
    function hash_func(key:integer):integer;
  end;


  TMyHashTable = class(THashTable)
    constructor create(m:integer);
    procedure delete_items(k1, k2:integer);
    procedure createRandom;
  end;

implementation

  constructor THashTable.create(_m : integer);
  var i : integer;
  begin
    inherited create;
    m := _m;
    GetMem(hash, sizeof(PNode) * m);
    clear;
  end;


  procedure THashTable.delete(key:integer);
  var i,h:integer;
  temp : PNode;
  begin
    h := hash_func(key);
    temp := hash[h];
    if(hash[h] = nil)then exit;
    if(hash[h]^.value.key = key) then
      hash[h] := temp^.next
    else begin
      while(temp^.next <> nil)do
      begin
        if(temp^.next^.value.key = key) then
          temp^.next := temp^.next^.next;
      temp := temp^.next;
      end;
    end;
  end;

  procedure THashTable.clear;
  var i : integer;
  begin
    for i := 1 to m do
      hash[i] := nil;
  end;

  function THashTable.hash_func(key:integer):integer;
  begin
    if(key < HASH_SIZE * m) then
      Result := key div HASH_SIZE + 1
    else
      Result := m;
  end;

  procedure THashTable.add(item : TItem);
  var hIndex : integer;
  temp : PNode;
  begin
    hIndex := hash_func(item.key);
    new(temp);
    temp^.value := item;
    temp^.next := hash[hIndex];
    hash[hIndex] := temp;
  end;

  function THashTable.find(key : integer; var item : TItem) : boolean;
  var hIndex : integer;
  temp : PNode;
  begin
    result := false;
    hIndex := hash_func(key);
    temp := hash[hIndex];
    while(temp <> nil) do
    begin
      if(temp^.value.key = key) then
      begin
        item := temp^.value;
        result := true;
        break;
      end;
      temp := temp^.next;
    end;
  end;

  procedure THashTable.print(memo : TMemo);
  var i : integer;
  txt : string;
  first : boolean;
  temp : PNode;
  begin
    memo.Clear;
    for i := 1 to m do
    begin
      txt := intToStr(i) + ' - ';
      first := false;
      temp := hash[i];
      while(temp <> nil) do
      begin
        if(not first) then
        begin
          first := true;
          txt := txt + temp^.value.str + '(' + intToStr(temp^.value.key) + ')';
        end else
          txt := txt + ', ' + temp^.value.str + '(' + intToStr(temp^.value.key) + ')';
        temp := temp^.next;
      end;
      memo.Lines.Add(txt);
    end;
  end;


  constructor TMyHashTable.create(m:integer);
  begin
    inherited create(m);
  end;

  procedure TMyHashTable.createRandom;
  var i:integer;
  item:TItem;
  begin
    randomize;
    for i := 1 to 10 do
    begin
      item.str := 'str' + intToStr(i);
      item.key := random(1000);
      add(item);
    end;
  end;

  procedure TMyHashTable.delete_items(k1, k2:integer);
  var i,hash_k1, hash_k2:integer;
  begin
    hash_k1 := hash_func(k1);
    hash_k2 := hash_func(k2);
    if(hash_k1 = hash_k2) then
    begin
      for i:= k1 to k2 do
        delete(i);
    end
    else begin
      for i:= k1 to hash_k1 * HASH_SIZE do
        delete(i);
      for i:= (hash_k2-1) * HASH_SIZE to k2 do
        delete(i);
      for i:= hash_k1 + 1 to hash_k2 - 1 do
        hash[i] := nil;
    end;
  end;

end.
