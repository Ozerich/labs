#ifndef TEST_H
#define TEST_H

#include "Trial.h"

class Test : public Trial
{
private:
	int questions_count;
	int correct_count;
public:
	Test();
	Test(const char *);
	~Test();

	void SetQuestionsCount(int);
	void SetCorrectCount(int);
	void SetResult(int, int);

	int GetQuestionsCount();
	int GetCorrectCount();

	virtual int GetMark();
	virtual void Print();
};

#endif