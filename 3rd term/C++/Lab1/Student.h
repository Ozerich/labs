#ifndef STUDENT_H
#define STUDENT_H

#include <string>
#include <cstdio>

using namespace std;

class Student
{
private:
	string name;
	string surname;
	string group;
public:
	Student();
	Student(Student &);
	Student(string , string , string );

	void SetName(string);
	void SetSurname(string);
	void SetGroup(string);

	string GetName();
	string GetSurname();
	string GetGroup();

	string ToString();
};

#endif