#ifndef STUDENT_H
#define STUDENT_H

#include <string.h>
#include <stdio.h>
#include <iostream>

using namespace std;

class Student
{
private:
	char *name;
	char *surname;
	char *group;

	int age;
public:
	Student();
	Student(Student &);
	Student(const char* , const char* , const char*, int);

	void SetName(const char *);
	void SetSurname(const char *);
	void SetGroup(const char *);
	void SetAge(const int);

	char* GetName();
	char* GetSurname();
	char* GetGroup();
	int GetAge();

	void ToString(ostream &out);

	~Student();
};

#endif
