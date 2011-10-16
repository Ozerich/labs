#include "Trial.h"

Trial::Trial()
{}

Trial::Trial(const char *name)
{
	SetName(name);
}

Trial::~Trial()
{
	delete name;
}

void Trial::SetName(const char *name)
{
	this->name = new char[strlen(name)];
	strcpy(this->name, name);
}

char* Trial::GetName()
{
	return this->name;
}