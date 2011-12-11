#include <cstdio>
#include <iostream>
#include <cstdlib>
#include "list.h"
#include "time.h"

using namespace std;

void PrintEven(void* x) {
	int v = ((ListItem<int>*)x)->value;
	if (v % 2 == 0)
		cout << v << " ";
}

void PrintOdd(void* x) {
	int v = ((ListItem<int>*)x)->value;
	if (v % 2 == 1)
		cout << v << " ";
}

void PrintAll(void* x) {
	int v = ((ListItem<int>*)x)->value;
    cout << v << " ";
}


int main()
{
    srand(time(NULL));
    List<int> *list = new List<int>();

    for(int i = 0; i < 10; i++)
        list->Add(rand() % 100);

    cout << "List: ";
    list->ForEach(PrintAll);

    cout << endl << "Odds: ";
    list->ForEach(PrintOdd);

    cout << endl << "Evens: ";
    list->ForEach(PrintEven);

    cin.get();
    delete list;

    return 0;
}
