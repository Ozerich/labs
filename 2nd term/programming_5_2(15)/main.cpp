#include <cstdio>
#include <cstdlib>
#include <conio.h>

#include "tree.h"

int main()
{
	Tree tree = Tree();
	int ch;
	FILE *f = fopen("input.txt", "r");
	while(fscanf(f, "%d", &ch) != -1)
		tree.Add(ch);
	tree.Print(Ascending);
	printf("\n");
	tree.Print();
	getch();
	return 0;
}