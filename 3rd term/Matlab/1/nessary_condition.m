%������������ ���� �� ���������� �� ������������ ��������
syms n lim;
lim = limit((n + 1) / (2 * n + 1), n, inf);
disp(lim)
if (lim ~= 0)
    disp('��� ����������')
else
    disp('��������� �������������� ������������');
end;

��� ����������