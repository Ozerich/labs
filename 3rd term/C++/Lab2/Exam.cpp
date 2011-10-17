#include "Exam.h"

Exam::Exam() : Trial()
{
	mark = 0;
}

Exam::Exam(const char *name): Trial(name){}

Exam::~Exam(){}

void Exam::SetMark(int _mark)
{
	mark = _mark;
}

int Exam::GetMark()
{
	return mark;
}

void Exam::Print()
{
    cout << "Information about exam: " << endl;
	cout << "Exam name: " << name << endl;
	cout << "Exam mark: " << GetMark() << endl;
	cout << endl;
}
