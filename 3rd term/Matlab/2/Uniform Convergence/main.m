hold on;
syms eps x k N

eps = 0.5;
N = round((1 + eps) / (2 * eps)) + 1;
x = 0:0.1:1;

plot(x, Calc(x, 1, inf) + eps, '-.g');
plot(x, Calc(x, 1, inf) - eps, '-.g');
plot(x, Calc(x, 1, inf) , '-');

for i = N : (N + 6)
    plot(x, Calc(x, 1, i), '-r');
end;

hold on;