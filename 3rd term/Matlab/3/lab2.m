syms x n k;
fx = 1 - 2 * x;

a0 = int(fx, -pi, pi)/(2*pi);
an = int(fx*cos(n*x), -pi, pi)/pi;
bn = int(fx*sin(n*x), -pi, pi)/pi;

Fx = a0;
E = an*cos(n*x)+bn*sin(n*x);

hold on;
xs = -3:0.1:3;
E = subs(E, x, xs);

for i = 1:3
    Fx = Fx + subs(E, n, i);
    plot(xs, Fx);
end
hold off;