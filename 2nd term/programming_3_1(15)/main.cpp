#include "stdio.h"
#include "stdlib.h"
#include <conio.h>

int main()
{
	int n, t;
	printf("N:");
	scanf("%d", &n);

	int *ms = (int*)malloc(n * sizeof(int));
	int *pos = (int*)malloc((n + 1) * sizeof(int));
	for(int i = 0; i < n; i++)
	{
		printf("ms[%d] = ", i);
		scanf("%d", &t);
		if(t < 1 || t > n)
		{
			i--;
			continue;
		}
		ms[i] = t;
		pos[t] = i;
	}

	int count = 0;
	for(int i = 0; i < n; i++)
	{
		if(ms[i] == i + 1)
			continue;
		count++;
		ms[pos[i + 1]] = ms[i];
	    pos[ ms[i] ] = pos[i + 1]; 
		ms[i] = i + 1;
		pos[i + 1] = i;
	}
	printf("Count: %d", count);
	getch();
	return 0;
}