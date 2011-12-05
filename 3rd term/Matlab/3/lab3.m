syms x;
xv = 1:10;
spc = 1/pi*double(int((5*x-1)*exp(-1i*pi*xv*x/pi), x, -pi, pi));

subplot(2,1,1);
bar(xv, abs(spc), 0.1);
subplot(2,1,2);
bar(xv, angle(spc), 0.1);

