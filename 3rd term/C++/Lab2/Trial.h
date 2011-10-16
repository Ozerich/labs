#ifndef TRIAL_H
#define TRIAL_H

#include <iostream>
#include <string.h>

using namespace std;

class Trial
{
protected:
	char *name;
public:
	Trial();
	Trial(const char *);
	
	~Trial();

	void SetName(const char *);
	char* GetName();

	virtual int GetMark() = 0;
	virtual void Print() = 0;
};

#endif TRIAL_H