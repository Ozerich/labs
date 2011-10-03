%Исследование ряда на сходимость по интегральному признаку Коши

syms n integral prev cur i flag
flag = true;
prev = 1 / ((1 + 1) * log(1 + 1) ^ 2);
for i = 2:1:10000
    cur = 1 / ((i + 1) * log(i + 1) ^ 2);
    if(cur > prev || cur < 0)
        flag = false;
        break;
    end
    prev = cur;
end;
integral = int(1 / ((n + 1) * log(n + 1) ^ 2), 1, inf);
if(integral == inf || flag)
    disp('Ряд расходится по интегральному признаку Коши');
else
    disp('Ряд сходится по интегральному признаку Коши');
end;

Ряд сходится по интегральному признаку Коши