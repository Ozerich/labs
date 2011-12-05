%Исследование сходимости ряда по признаку Лейбница

syms n v1 v2 prev cur lim flag
for v1 = 1:1:1000
    flag = true;
    prev = log(2 * v1 / (v1 + 2)) ^ v1;
    for v2 = v1+1:1:v1+1000
        cur = log(2 * v2 / (v2 + 2)) ^ v2;
        if(cur > prev)
            flag = false;
            break;
        end;
    end;
    if(flag == true)
        break;
    end;
end;
lim = limit(cos(n) / (n ^ 2), n, inf);
if lim == 0 && flag
    disp('Ряд сходится');
else 
    disp('Ряд расходится');
end;