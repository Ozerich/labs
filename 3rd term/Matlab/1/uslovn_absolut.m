%������������ ��� �� �������� � ���������� ����������
syms n lim
lim = limit(log(2 * n / (n + 2)) ^ n, n, inf);
if lim == 0 
    lim = limit((log(2 * n / (n + 2)) ^ n) ^ (1 / n), n, inf);
    if double(lim) < 1 
        disp('��� �������� ���������')
    elseif double(lim) > 1
        disp('��� �������� �������');
    else
        disp('��������� �������������� ������������');
    end;
else 
    disp('��� ����������');
end;