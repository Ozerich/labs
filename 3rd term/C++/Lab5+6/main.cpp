#include <cstdio>
#include <iostream>
#include <cstdlib>
#include "list.h"
#include "time.h"
#include <fstream>
#include "Student.h"

using namespace std;

ofstream fout("output.txt");

void PrintAbove(void* x) {
	Student st = ((ListItem<Student>*)x)->value;
	if (st.GetAge() > 19)
	{
		st.ToString(fout);
		fout << endl;
	}}

void PrintBelow(void* x) {
	Student st = ((ListItem<Student>*)x)->value;
	if (st.GetAge() <= 19)
	{
		st.ToString(fout);
		fout << endl;
	}
}


int main()
{
    srand(time(NULL));
    List<Student> *list = new List<Student>();
	
	Student *st1 = new Student("Petya", "Ivanov", "052004", 20);
	Student *st2 = new Student("Vasya", "Petrov", "052003", 18);
	Student *st3 = new Student("Imya", "Bezfamilny", "052001", 21);

	list->Add(*st1);
	list->Add(*st2);
	list->Add(*st3);

    fout << "Student with age above 19: " << endl << endl;
    list->ForEach(PrintAbove);

    fout << endl << "Student with age below 19: " << endl << endl;
    list->ForEach(PrintBelow);

    delete list;

    return 0;
}
