#include "stdio.h"
#include "stdlib.h"
#include "conio.h"

int max(int a, int b)
{
	return (a < b) ? b : a;
}

int main()
{
	int n,m,k,l;
	printf("Print N, M: ");
	scanf("%d %d", &n, &m);
	int **ms = (int**)malloc(sizeof(int*) * n);
	for(int i = 0; i < n; i++)
		ms[i] = (int*)malloc(sizeof(int) * m);
	printf("Print K, L: ");
	scanf("%d %d", &k, &l);
	for(int i = 0; i < n; i++)
		for(int j = 0; j < m; j++)
			ms[i][j] = max(abs(k - i), abs(l - j)) + 1;
	for(int i = 0; i < n; i++)
	{
		for(int j = 0; j < m; j++)
			printf("%d\t", ms[i][j]);
		printf("\n");
	}
	for(int i = 0; i < n; i++)
		free(ms[i]);
	free(ms);
	getch();
	return 0;
}