#include <vector>
#include "Student.h"

using namespace std;



class Group
{
private:
	vector<Student> data;
public:
	Student operator [] (int ind)
	{
		return data[ind];
	}

	void Add(Student st)
	{
		data.push_back(st);
	}

	void forEach(void (*func)(Student))
	{
		for(int i = 0; i < data.size(); i++)
			func(data[i]);
	}
};