syms n eps s;

n = 1;
eps = 0.001;
s = 0;

while double(abs(Calc(n))) > double(eps)
    s = s + Calc(n);
    n = n + 1;
end;

disp(fprintf('%s %d', 'n = ', n));
disp(fprintf('%s %f', 'Result: ', double(s)));