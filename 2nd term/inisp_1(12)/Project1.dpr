program Project1;

{$APPTYPE CONSOLE}

uses
  SysUtils,
  Edge in 'Edge.pas',
  EdgeList in 'EdgeList.pas',
  Graph in 'Graph.pas',
  Vertex in 'Vertex.pas',
  VertexList in 'VertexList.pas';

function InputInt(hint: string; var value: integer): boolean;
begin
  write('...' + hint + ' ');
  readln(value);
  Result := true;
end;

function InputString(hint: string; var value: string): boolean;
begin
  write('...' + hint + ' ');
  readln(value);
  Result := true;
end;

var
  gr: TGraph;
  cmd, s1, s2: string;
  found, weight: integer;
  vert: TVertex;
  edg: TEdge;

begin
  gr := TGraph.Create;
  Writeln('Maksim Shyrko, group 0502001, Lab 1');
  Readln;
  while (true) do
  begin
    Writeln('Commands: add_vertex, find_vertex, delete_vertex, add_edge, find_edge, delete_edge, print_graph, exit');
    Write('? ');
    Readln(cmd);
    if(cmd = 'add_vertex') then
    begin
      InputString('Vertex name:', s1);
      gr.AddVertex(s1);
    end
    else if(cmd = 'find_vertex') then
    begin
      InputString('Vertex name:', s1);
      found := gr.FindVertex(s1, vert);
      if(found = -1) then writeln('No found') else writeln('Vertex id: ' + IntToStr(vert.Id));
    end
    else if(cmd = 'delete_vertex') then
    begin
      InputString('Vertex name:' ,s1);
      gr.DeleteVertex(s1);
    end
    else if(cmd = 'add_edge') then
    begin
      InputString('Vertex 1 name: ', s1);
      InputString('Vertex 2 name: ', s2);
      InputInt('Edge weight: ', weight);
      gr.AddEdge(s1, s2, weight);
    end
    else if(cmd = 'find_edge') then
    begin
      InputString('Vertex 1 name: ', s1);
      InputString('Vertex 2 name: ', s2);
      found := gr.FindEdge(s1, s2, edg);
      if(found = -1) then writeln('No found') else writeln('Edge Weight: ' + IntToStr(edg.weight));
    end
    else if(cmd = 'delete_edge') then
    begin
      InputString('Vertex 1 name: ', s1);
      InputString('Vertex 2 name: ', s2);
      gr.DeleteEdge(s1, s2);
    end
    else if(cmd = 'print_graph') then gr.Print
    else if(cmd = 'exit') then break
    else Writeln('Incorrect command');
    Writeln;
  end;

end.
