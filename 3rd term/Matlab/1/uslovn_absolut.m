%Исследование ряд на условную и абсолютную сходимость
syms n lim
lim = limit(log(2 * n / (n + 2)) ^ n, n, inf);
if lim == 0 
    lim = limit((log(2 * n / (n + 2)) ^ n) ^ (1 / n), n, inf);
    if double(lim) < 1 
        disp('Ряд сходится абсолютно')
    elseif double(lim) > 1
        disp('Ряд сходится условно');
    else
        disp('Требуется дополнительное исследование');
    end;
else 
    disp('Ряд расходится');
end;