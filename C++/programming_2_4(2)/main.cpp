#include <stdio.h>
#include <stdlib.h>
#include <conio.h>

char *text;

void input()
{
	text = new char[10000];
	FILE *f = fopen("input.txt", "r");
	fgets(text, 10000, f);
	fclose(f);
}

int strlen(char *str)
{
	int i;
	for(i = 0; str[i] != '\0'; i++);
	return i;
}

bool is_letter(char s)
{
	return (s >= 'a' && s <= 'z') || (s >= 'A' && s <= 'Z');
}

bool is_digit(char s)
{
	return (s >= '0' && s <= '9');
}

int get_word_count()
{
	int count = 0;
	bool word_begin = false;
	for(int i = 0; i < strlen(text); i++)
	{
		if(is_letter(text[i]))
		{
			if(!word_begin)
			{
				word_begin = true;
				count++;
			}
		}
		else
			word_begin = false;
	}
	return count;
}

int get_num_sum()
{
	char number[100];
	int dind = 0, result = 0;
	for(int i = 0; i <= strlen(text); i++)
	{
		if(i < strlen(text) && is_digit(text[i]))
			number[dind++] = text[i];
		else
		{
			if(dind != 0)
			{
				number[dind++] = '\0';
				result += atoi(number);
			}
			dind = 0;
		}
	}
	return result;
}

int main()
{
	input();
	int word_count = get_word_count();
	printf("Words = %d\n", word_count);
	int num_sum = get_num_sum();
	printf("Sum of numbers = %d\n", num_sum);
	getch();
	return 0;
}