function y = myspectre(k)
    l = pi;
    syms x;
    y = 1/2*double(int(smdl(x)*exp(-1i*pi*k*x/l), 0, l));
    %y = 1/2*double(limit(int(smdl(x)*exp(1i*pi*k*x/l)), x, l));
end