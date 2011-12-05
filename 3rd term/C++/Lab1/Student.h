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
public:
	Student();
	Student(Student &);
	Student(const char* , const char* , const char* );

	void SetName(const char *);
	void SetSurname(const char *);
	void SetGroup(const char *);

	char* GetName();
	char* GetSurname();
	char* GetGroup();

	void ToString();

	~Student();
};

#endif
