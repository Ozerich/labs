function y = Calc(n)
    syms f x;
    %Average member
    f = ((-1) ^ (n + 1)) / (gamma(n - 2) * (2 * n - 1) * (4 ^ (2 * n - 1))); 
    %Integrate function
    y = int(f, 0.0, double(1 / 4));
end

