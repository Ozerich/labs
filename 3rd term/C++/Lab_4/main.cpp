#include <cstdio>
#include <iostream>
#include <cstdlib>
#include "list.h"
#include "time.h"
#include <fstream>
#include "Student.h"
#include <vector>
#include <algorithm>
#include "Group.h"

using namespace std;

ofstream fout("output.txt");

void PrintAbove(Student st) {
	if (st.GetAge() > 19)
	{
		st.ToString(fout);
		fout << endl;
	}
}

void PrintBelow(Student st) {
	if (st.GetAge() <= 19)
	{
		st.ToString(fout);
		fout << endl;
	}
}


int main()
{
    srand(time(NULL));
    Group group;
	
	Student st1 = Student("Petya", "Ivanov", "052004", 20);
	Student st2 = Student("Vasya", "Petrov", "052003", 18);
	Student st3 = Student("Imya", "Bezfamilny", "052001", 21);
	
	group.Add(st1);
	group.Add(st2);
	group.Add(st3);

    fout << "Student with age above 19: " << endl << endl;
	group.forEach(PrintAbove);

    fout << endl << "Student with age below 19: " << endl << endl;
	group.forEach(PrintBelow);
	

    return 0;
}
