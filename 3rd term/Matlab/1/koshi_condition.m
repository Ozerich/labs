%������������ ���� �� ���������� �� �������� ����

syms n lim
lim = limit((((n + 1) / (2 * n - 3))^(n^2)) ^ (1 / n), n, inf);
if double(lim) < 1
    disp('��� �������� �� �������� ����');
elseif double(lim) > 1
    disp('��� ���������� �� �������� ����');
else
    disp('���������� �������������� ������������');
end;    

��� �������� �� �������� ����