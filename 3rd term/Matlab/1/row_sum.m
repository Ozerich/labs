%Подсчет суммы ряда по определению
syms sm n s k;
sm = symsum(3/(9*n^2+3*n-2), n, 1, inf)
s = limit(sm, inf)
disp('Сумма ряда равна: ');
disp(s);

Сумма ряда равна: 1/2



