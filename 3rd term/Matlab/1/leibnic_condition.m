%Исследование сходимости ряда по признаку Лейбница
syms n lim
lim = limit(cos(n) / (n ^ 2), n, inf);
if lim == 0 
    disp('Ряд сходится');
else 
    disp('Ряд расходится');
end;