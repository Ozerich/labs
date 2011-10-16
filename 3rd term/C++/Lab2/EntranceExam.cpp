#include "EntranceExam.h"

EntranceExam::EntranceExam() : Exam() {}

EntranceExam::EntranceExam(const char *name, const char *_subject) : Exam(name)
{
	SetSubject(_subject);
}

EntranceExam::~EntranceExam(){};

void EntranceExam::SetSubject(const char *_subject)
{
	subject = new char[strlen(_subject)];
	strcpy(subject, _subject);	
}

char* EntranceExam::GetSubject()
{
	return subject;
}

void EntranceExam::Print()
{
	cout << "Exam subject: " << GetSubject() << endl;
	Exam::Print();
}