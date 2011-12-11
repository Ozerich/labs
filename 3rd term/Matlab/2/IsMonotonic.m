function res = IsMonotonic(exp)
    syms d s n x;
    d = diff(exp);
    disp(d);
    s = solve(d);
    x = symsum(d, 1, 1);
    res = isempty(s) && (double(x) < 0);
end