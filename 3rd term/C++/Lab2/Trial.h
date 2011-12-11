#ifndef TRIAL_H
#define TRIAL_H

#include <iostream>
#include <string.h>

using namespace std;

class Trial
{
protected:
    //Trial name
	char *name;
public:
	Trial();
	Trial(const char *);

	~Trial();

	void SetName(const char *);
	char* GetName();

    //Return result of trial
	virtual int GetMark() = 0;
	//Print to console
	virtual void Print() = 0;
};

#endif TRIAL_H
