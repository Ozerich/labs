%������������ ���������� ���� �� �������� ���������
syms n lim
lim = limit((gamma(n) / (gamma(n * 3 - 1))) / (gamma(n - 1) / (3 * gamma(n - 1))), n, inf);
disp(lim)   
if double(lim) < 1
    disp('�� �������� ��������� ��� ��������');
elseif double(lim) > 1
    disp('�� �������� ��������� ��� ����������');
else 
    disp('��������� �������������� ������������');
end;

