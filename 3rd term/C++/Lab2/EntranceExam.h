#ifndef ENTRANCE_EXAM_H
#define ENTRANCE_EXAM_H

#include "Exam.h"

class EntranceExam : public Exam
{
private:
    //Subject of exam
	char *subject;
public:
	EntranceExam();
	EntranceExam(const char *, const char *);
	~EntranceExam();

	void SetSubject(const char *);
	char* GetSubject();

	virtual void Print();
};

#endif
