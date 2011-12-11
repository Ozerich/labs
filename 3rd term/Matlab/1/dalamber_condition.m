%Исследование сходимости ряда по признаку Д'аламбера

syms n lim
lim = limit((gamma(n) / (gamma(n * 3 - 1))) / (gamma(n - 1) / (3 * gamma(n - 1))), n, inf);
disp(lim)   
if double(lim) < 1
    disp("По признаку Д'аламбера ряд сходится");
elseif double(lim) > 1
    disp("По признаку Д'аламбера ряд расходится");
else 
    disp("Требуется дополнительное исследование");
end;

