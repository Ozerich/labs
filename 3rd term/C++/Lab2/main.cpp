#include <cstdio>
#include "Test.h"
#include "Trial.h"
#include "Exam.h"
#include "EntranceExam.h"

using namespace std;

int main(int argc, char **argv)
{
	Test *test = new Test("Test 1");
	test->SetResult(10, 4);

	Exam *exam = new Exam("Exam 1");
	exam->SetMark(8);

	EntranceExam *entranceExam = new EntranceExam("Entrance Exam 2", "Mathematics");
	entranceExam->SetMark(5);

    //Testing virtual method Print()
	Trial **trials = new Trial*[3];
	trials[0] = test;
	trials[1] = exam;
	trials[2] = entranceExam;

	for(int i = 0; i < 3; i++)
		trials[i]->Print();

	cin.get();cin.get();
	return 0;
}
