syms n eps fx x t cur

%�������� ��������
fx = exp(1) ^ (-(x ^ 2));
eps = 0.001;
prev = 0;
n = 0;
cur = 0;

%���� �� ������������� ������� �� ����������� ��������
while(cur == 0 || abs(cur - prev) >= eps)
    %���������� ����������� ��������
	prev = cur;
    while(prev == cur)
		%��������� �������� n-�� ����� ���� ����������� � ������� ���������� �� �������
        cur = subs(int(taylor(fx, n), 0, 1 / 4));
        n = n + 1;
    end;  
end;

%����� � 10 ������� ����� �����
fprintf('Result: %.10f\n', cur);