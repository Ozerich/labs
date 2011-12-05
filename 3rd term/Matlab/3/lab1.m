syms x n k;
fx =  1 - x * 2;

a0 = int(fx, -pi, pi)/(2*pi);
an = int(fx*cos(n*x), -pi, pi)/pi;
bn = int(fx*sin(n*x), -pi, pi)/pi;

Fx = a0 + symsum(an*cos(n*x)+bn*sin(n*x), 1, 3);
disp(Fx)