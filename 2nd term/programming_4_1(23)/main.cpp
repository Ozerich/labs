#include <stdio.h>
#include <stdlib.h>
#include <conio.h>

int n;
int *m;

char ***data;

void input()
{
	FILE *f = fopen("input.txt", "r");
	fscanf(f, "%d", &n);
	data = (char***)malloc(n * sizeof(char**));
	m = (int*)malloc(n * sizeof(int));
	for(int i = 0; i < n; i++)
	{
		fscanf(f, "%d", &m[i]);
		data[i] = (char**)malloc(m[i] * sizeof(char*));
		for(int j = 0; j < m[i]; j++)
		{
			data[i][j] = new char[100];
			fscanf(f, "%s", data[i][j]);
		}

	}
	fclose(f);
}

void output()
{
	for(int i = 0; i < n; i++)
	{
		printf("matrix[%d] = ", i + 1);
		for(int j = 0; j < m[i]; j++)
			printf("%s\t", data[i][j]);
		printf("\n");
	}
}

int strlen(char *str)
{
	int i;
	for(i = 0; str[i] != '\0'; i++);
	return i;
}

bool pal(char *str)
{
	for(int i = 0; i < strlen(str) / 2; i++)
		if(str[i] != str[strlen(str) - 1 - i])
			return false;
	return true;
}

void delete_elem(int n_ind, int m_ind)
{
	for(int i = m_ind + 1; i < m[n_ind]; i++)
		data[n_ind][i - 1] = data[n_ind][i];
	m[n_ind]--;
}

void solve()
{
	for(int i = 0; i < n; i++)
	{
		int j = 0;
		while(j < m[i] && m[i] > 0)
		{
			if(pal(data[i][j]))
				delete_elem(i, j);
			else
				j++;
		}
	}
}

int main()
{
	input();
	printf("Input data:\n");
	output();
	solve();
	printf("Result:\n");
	output();
	for(int i = 0; i < n; i++)
	{
		for(int j = 0; j < m[i]; j++)
			free(data[i][j]);
		free(data[i]);
	}
	getch();
	return 0;
}