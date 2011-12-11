#include <cstdio>
#include <cstdlib>
#include <conio.h>

using namespace std;

const char *fname = "input.txt";

char *text;

int strlen(char *str)
{
	int i;
	for(i = 0; str[i] != '\0'; i++);
	return i;
}

void input()
{
	text = new char[1000];
	FILE *f = fopen(fname, "r");
	fgets(text, 1000, f);
	fclose(f);
}

int check()
{
	bool brackets_exist = false;
	for(int i = 0; i < strlen(text); i++)
		if(text[i] == '(' || text[i] == ')')
		{
			brackets_exist = true;
			break;
		}
	if(brackets_exist == false)
		return 0;
	int count = 0;
	for(int i = 0; i < strlen(text); i++)
	{
		if(text[i] == '(')
			count++;
		else if(text[i] == ')')
			count--;
		if(count < 0)
			return -1;
	}
	if(count != 0) 
		return -1;
	else
		return 1;
}

bool only_brackets(char* s)
{
	for(int i = 0; i < strlen(s); i++)
		if(s[i] != '(' && s[i] != ')')
			return false;
	return true;
}

int get_bracket_end(int beg)
{
	int count = 1;
	for(int i = beg + 1; i < strlen(text); i++)
	{
		if(text[i] == '(')
			count++;
		else if(text[i] == ')')
			count--;
		if(count == 0)
			return i + 1;
	}
}

char* strcat(char* text, int beg, int end)
{
	char* result = new char[1000];
	for(int i = beg + 1; i <= end + 1; i++)
		result[i - beg - 1] = text[i];
	result[end - beg - 1] = '\0';
	return result;
}

bool find_bracket(int &left, int &right)
{
	left = right = -1;
	int res_depth = 0, count = 0, bracket_end, depth = 0;
	char *s;
	for(int i = 0; i < strlen(text); i++)
		if(text[i] == '(')
		{
			depth++;
			bracket_end = get_bracket_end(i);
			s = strcat(text, i, bracket_end - 1);
			if(only_brackets(s))
				continue;
			if(depth > res_depth)
			{
				left = i;
				right = bracket_end - 1;
				res_depth = depth;
			}
		}
		else if(text[i] == ')')
			depth--;
	return (left != -1 && right != -1);
}

void delete_char(char *text, int pos)
{
	for(int i = pos + 1; i < strlen(text); i++)
		text[i - 1] = text[i];
	text[strlen(text) - 1] = '\0';
}

void str_delete(char *text, int left, int len)
{
	int pos = left;
	for(int i = 0; i < len; i++)
		if(text[pos] == '(' || text[pos] == ')')
			pos++;
		else
			delete_char(text, pos);
}

void solve()
{
	int left, right;
	bool found = true;
	while(found)
	{
		found = find_bracket(left, right);
		str_delete(text, left + 1, right - left - 1);
	}
}

int main()
{
	input();
	int check_result = check();
	if(check_result == -1)
		printf("Bad Input");
	else if(check_result == 0)
		printf("No brackets");
	else
	{
		solve();
		printf("Result: %s", text);
	}
	getch();
	return 0;
}