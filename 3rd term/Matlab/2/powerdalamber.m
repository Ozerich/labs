syms n R lim

%??????? ?????? ??????????
R = limit( ((3 ^ n) * (n + 2)) / ((3 ^ (n - 1)) * (n + 1)), inf);
disp('Radius');
disp(R);

%???????? ????? ??????? ?? ????????
if((limit(3 / (n + 1), n, inf) == 0) && IsMonotonic(3 / (n + 1)))
    disp('Include left border');
else
    disp('Exclude left border');
end;

%???????? ?????? ??????? ?? ???????? ?????????
lim = double(limit(n / (n + 1), inf));
if((isinf(lim) == false) && (lim > 0))
    disp('Exclude right border');
else
    disp('Include right border');
end;