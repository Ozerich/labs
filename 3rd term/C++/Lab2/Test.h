#ifndef TEST_H
#define TEST_H

#include "Trial.h"

class Test : public Trial
{
private:
    //Questions count in this test
	int questions_count;
	//Correct answers
	int correct_count;
public:
	Test();
	Test(const char *);
	~Test();

	void SetQuestionsCount(int);
	void SetCorrectCount(int);

	//Set result of test (1st param - answers count, 2nd param - correct answers)
	void SetResult(int, int);

	int GetQuestionsCount();
	int GetCorrectCount();

	virtual int GetMark();
	virtual void Print();
};

#endif
