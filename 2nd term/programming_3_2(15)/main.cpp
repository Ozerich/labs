#include "stdio.h"
#include "stdlib.h"
#include <conio.h>

int main()
{
	int n, t;
	printf("N: ");
	scanf("%d", &n);
	int ***ms = (int***)malloc(n * sizeof(int**));
	for(int i = 0; i < n; i++)
	{
		ms[i] = (int**)malloc(n * sizeof(int*));
		for(int j = 0; j < n; j++)
			ms[i][j] = (int*)malloc(n * sizeof(int));
	}
	for(int i = 0; i < n; i++)
		for(int j = 0; j < n; j++)
			for(int z = 0; z < n; z++)
			{
				printf("cube[%d][%d][%d]=", i + 1, j + 1, z + 1);
				scanf("%d", &t);
				if(t == 0 || t == 1)
					ms[i][j][z] = t;
				else
				{
					printf("Type 0 or 1\n");
					z--;
				}
			}
	for(int i = 0; i < n; i++)
		for(int j = 0; j < n; j++)
			for(int z = 0; z < n; z++)
			{
				if(ms[i][j][z] == 1)
					break;
				if(z == n - 1)
				{
					printf("YES - (%d, %d, 1)", i + 1, j + 1);
					getch();
					return 0;
				}
			}
	for(int j = 0; j < n; j++)
		for(int z = 0; z < n; z++)
			for(int i = 0; i < n; i++)
			{
				if(ms[i][j][z] == 1)
					break;
				if(i == n - 1)
				{
					printf("YES - (1, %d, %d)", j + 1, z + 1);
					getch();
					return 0;
				}
			}
	for(int z = 0; z < n; z++)
		for(int i = 0; i < n; i++)
			for(int j = 0; j < n; j++)
			{
				if(ms[i][j][z] == 1)
					break;
				if(i == n - 1)
				{
					printf("YES - (%d, 1, %d)", i + 1, z + 1);
					getch();
					return 0;
				}
			}
	printf("NO");
	getch();
	return 0;
}