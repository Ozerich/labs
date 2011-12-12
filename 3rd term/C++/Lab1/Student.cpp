#include "Student.h"

Student::Student()
{
}

Student::Student(const char* name, const char* surname, const char* group, int age)
{
	SetName(name);
	SetSurname(surname);
	SetGroup(group);
	SetAge(age);
	cout << "Constuctor for " << name << " " << surname << " runned" << endl << endl;
}

Student::Student(Student &student)
{
	SetName(student.GetName());
	SetSurname(student.GetSurname());
	SetGroup(student.GetGroup());
	cout << "Constuctor(copy) for " << name << " " << surname << " runned" << endl << endl;
}

void Student::SetAge(const int _age)
{
	age = _age;
}

void Student::SetName(const char *name)
{
	if(strlen(name) > 0)
	{
		this->name = new char[strlen(name)];
		strcpy(this->name, name);
	}
}

void Student::SetSurname(const char *surname)
{
	if(strlen(surname) > 0)
	{
		this->surname = new char[strlen(surname)];
		strcpy(this->surname, surname);
	}
}

void Student::SetGroup(const char *group)
{
	for(unsigned int i = 0; i < strlen(group); i++)
		if(group[i] < '0' || group[i] > '9')
			return;
	if(strlen(group) > 0)
	{
		this->group = new char[strlen(group)];
		strcpy(this->group, group);
	}
}

char* Student::GetGroup()
{
	return group;
}

char* Student::GetName()
{
	return name;
}

char* Student::GetSurname()
{
	return surname;
}

void Student::ToString()
{
	cout << "Student's name: " << name << endl << "Student's surname: " << surname << endl << "Student's group: " << group << endl;
}

Student::~Student()
{
	cout << "Destructor for " << name << " " << surname << " runned" << endl;
	
	delete name;
	delete surname;
	delete group;
}
