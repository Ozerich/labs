#include "Test.h"
#include <math.h>


Test::Test() : Trial()
{
	questions_count = 0;
	correct_count = 0;
}

Test::Test(const char *name) : Trial(name) {}

Test::~Test(){}

void Test::SetQuestionsCount(int count)
{
	if(count > 0)
		questions_count = count;
}

void Test::SetCorrectCount(int count)
{
	if(count >= 0 && count <= questions_count)
		correct_count = count;
}

void Test::SetResult(int q_count, int c_count)
{
	SetQuestionsCount(q_count);
	SetCorrectCount(c_count);
}

int Test::GetQuestionsCount()
{
	return questions_count;
}

int Test::GetCorrectCount()
{
	return correct_count;
}

int Test::GetMark()
{
    //Convert to 10 ball system
	return (int)((double)10 / questions_count) * correct_count;
}

void Test::Print()
{
    cout << "Information about test: " << endl;
	cout << "Test name: " << GetName() << endl;
	cout << "Test Result: " << GetMark() << " (" << GetCorrectCount() << "/" << GetQuestionsCount() << ")" << endl;
	cout << endl;
}

