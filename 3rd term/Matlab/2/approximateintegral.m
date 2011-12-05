syms n eps fx x t cur

%исходный интеграл
fx = exp(1) ^ (-(x ^ 2));
eps = 0.001;
prev = 0;
n = 0;
cur = 0;

%пока не удовлетворяют условию на необходимую точность
while(cur == 0 || abs(cur - prev) >= eps)
    %сохранение предудущего значения
	prev = cur;
    while(prev == cur)
		%получение значения n-го члена ряда полученного с помощью разложения по Тейлору
        cur = subs(int(taylor(fx, n), 0, 1 / 4));
        n = n + 1;
    end;  
end;

%вывод с 10 знаками после точки
fprintf('Result: %.10f\n', cur);