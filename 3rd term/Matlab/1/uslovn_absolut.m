%Исследование ряда на условную и абсолютную сходимость

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
lim = limit(log(2 * n / (n + 2)) ^ n, n, inf);
if lim == 0 && flag
    lim = limit((log(2 * n / (n + 2)) ^ n) ^ (1 / n), n, inf);
    if double(lim) < 1 
        disp('Ряд сходится абсолютно')
    elseif double(lim) > 1
        disp('Ряд сходится условно');
    else
        disp('Необходимо дополнительное исследование');
    end;
else 
    disp('Ряд рассходится, так как не выполняются условия признака Лейбница');
end;
