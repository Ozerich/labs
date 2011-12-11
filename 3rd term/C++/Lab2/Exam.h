#ifndef EXAM_H
#define EXAM_H

#include "Trial.h"

class Exam : public Trial
{
private:
    //Result mark of exam
	int mark;
public:
	Exam();
	Exam(const char *);
	~Exam();

	void SetMark(int);

	virtual int GetMark();
	virtual void Print();
};

#endif
