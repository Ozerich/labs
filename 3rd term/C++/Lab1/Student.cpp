#include "Student.h"

Student::Student()
{
	name = surname = group = "";
}

Student::Student(string name, string surname, string group)
{
	SetName(name);
	SetSurname(surname);
	SetGroup(group);
}

Student::Student(Student &student)
{
	SetName(student.GetName());
	SetSurname(student.GetSurname());
	SetGroup(student.GetGroup());
}

void Student::SetName(string name)
{
	if(name.size() > 0)
		this->name.assign(name.begin(), name.end());
}

void Student::SetSurname(string surname)
{
	if(surname.size() > 0)
		this->surname.assign(surname.begin(), surname.end());
}

void Student::SetGroup(string group)
{
	for(unsigned int i = 0; i < group.size(); i++)
		if(group[i] < '0' || group[i] > '9')
			return;
	if(group.size() > 0)
		this->group.assign(group.begin(), group.end());
}

string Student::GetGroup()
{
	return group;
}

string Student::GetName()
{
	return name;
}

string Student::GetSurname()
{
	return surname;
}

string Student::ToString()
{
	string result;
	
	result = "Student's name: " + name + "\r\n";
	result += "Student's surname: " + surname + "\r\n";
	result += "Student's group: " + group + "\r\n";

	return result;
}