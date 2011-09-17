#include "stdio.h"
#include "stdlib.h"
#include "string.h"
#include <conio.h>

int n;
char **ms;
int *used, *result;

int rec(int ind)
{
	if(ind == n)
	{
		if(ms[result[n - 1]][strlen(ms[result[n - 1]]) - 1] == ms[result[0]][0])
			return 1;
		else
			return 0;
	}
	else
	{
		for(int i = 0; i < n; i++)
		{
			if(ind != 0 && (used[i] || ms[i][0] != ms[result[ind - 1]][strlen(ms[result[ind - 1]]) - 1]))
				continue;
			result[ind] = i;
			used[i] = 1;
			if(rec(ind + 1) == 1)
				return 1;
			used[i] = 0;
		}
	}
	return 0;
}

int main()
{
	printf("N: ");
	scanf("%d", &n);
	ms = (char**)malloc(n * sizeof(char*));
	used = (int*)malloc(n * sizeof(int));
	result = (int*)malloc(n * sizeof(int));
	for(int i = 0; i < n; i++)
	{
		used[i] = 0;
		ms[i] = (char*)malloc(8 * sizeof(char));
		printf("String %d: ", i + 1);
		scanf("%s", ms[i]);
	}
	int res = rec(0);
	if(res == 0)
		printf("NO");
	else
	{
		printf("YES - ");
		for(int i = 0; i < n; i++)
			printf("%s -> ", ms[result[i]]);
		printf("%s", ms[result[0]]);
	}
	getch();
	return 0;
}