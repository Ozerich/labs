#include "stdio.h"
#include "stdlib.h"
#include <conio.h>

int main()
{
	char infile[100], outfile[100];
	printf("Input file: ");
	scanf("%s", infile);
	printf("Output file: ");
	scanf("%s", outfile);

	char ch, last_char = '.';

	FILE *fin = fopen(infile, "r+");
	FILE *fout = fopen(outfile, "w+");

	while(fscanf(fin, "%c", &ch) != -1)
	{
		if(last_char == '.' && ch >= 'à' && ch <= 'ÿ')
			ch = 'À' + (ch - 'à');
		if(ch != ' ')
			last_char = ch;
		fprintf(fout, "%c", ch);
	}
	
	fclose(fin);
	fclose(fout);
	
	printf("Operation completed");
	getch();
	return 0;
}