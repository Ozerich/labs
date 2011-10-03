function res = lim
syms n k
k = limit((((n + 1) / (2 * n - 3))^(n^2)) ^ (1 / n), n, inf);
if(k == inf)
    res = 99999;
else
    res = double(k);
end
end

