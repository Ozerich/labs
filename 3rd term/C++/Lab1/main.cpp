#include "Student.h"
#include <cstdio>
#include <string>
#include <iostream>

using namespace std;

int main()
{
	Student *st1 = new Student("Vital", "Ozierski", "052004");
	st1->ToString();

	Student *st2 = new Student(*st1);
	st2->ToString();

	st1->SetName("Name");

	cout << endl << "After changing" << endl;
	st1->ToString();
	cout << endl;

	delete st1;
	delete st2;

	return 0;
}
