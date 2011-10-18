function res = Calc(x, a, b)
    syms n; 
    res = double(symsum((((-1) .^ n) * (x.^n)) / (2.^n - 3), n,a, b));
end