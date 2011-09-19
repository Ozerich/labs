#include "Student.h"
#include <cstdio>
#include <string>
#include <iostream>

using namespace std;

int main()
{
	Student *st1 = new Student("Vital", "Ozierski", "052004");
	cout << st1->ToString() << endl;

	Student *st2 = new Student(*st1);
	cout << st2->ToString() << endl;

	st1->SetName("Name");
	st1->SetName("Surname");
	st1->SetGroup("group");

	cin.get();cin.get();
	return 0;
}