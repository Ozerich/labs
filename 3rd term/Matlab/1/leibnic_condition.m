%������������ ���������� ���� �� �������� ��������
syms n lim
lim = limit(cos(n) / (n ^ 2), n, inf);
if lim == 0 
    disp('��� ��������');
else 
    disp('��� ����������');
end;