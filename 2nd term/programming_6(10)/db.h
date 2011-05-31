#include <cstdio>
#include <cstdlib>

class Question
{
public:
    char **answers;
    int answers_count;
    int correct_answer;
    char *text;
    
    Question(char *text_)
    {
        text = new char[100];
        strcpy(text, text_);
    }
    
    Question()
    {
        answers = new char*[100];
        for(int i = 0; i < 100; i++)
            answers[i]  = new char[1000];
        text = new char[100];
    }
    
    void addAnswer(char *text)
    {
       strcpy(answers[answers_count++], text);
    }
};


struct UserResult
{
	char *name;
	int result;

	UserResult()
	{
		name = new char[1000];
		result = 0;
	}
	
	UserResult(char *_name, int _result)
	{
		name = new char[1000];
		strcpy(name, _name);
		result = _result;
	}
};

int max(int a, int b)
{
	return (a<b)?b:a;
}

class Test
{
public:
    Question *questions;
    int questions_count;
	UserResult *results;
	int result_count;
    char *name;
    
    Test(char *name_)
    {
        name = new char[100];
		results = new UserResult[1000];
        strcpy(name, name_);
        questions_count = result_count = 0;
    }
    
    Test()
    {
		results = new UserResult[1000];
        name = new char[100];
		questions_count = result_count = 0;
    }

	void AddResult(char *user_name, int result)
	{
		for(int i = 0; i < result_count; i++)
			if(strcmp(results[i].name, user_name) == 0)
			{
				results[i].result = max(results[i].result, result);
				return;
			}
		results[result_count++] = UserResult(user_name, result);
	}
};



class DB
{
public:
    Test *tests;
	int tests_count;
    void deleteTest(int);
    DB(); 
    ~DB();   
};

void DB::deleteTest(int index)
{
    for(int i = index; i < tests_count - 1; i++)
        tests[i] = tests[i + 1];
    tests_count--;
}


DB::DB()
{
    const char *filename = "base.txt";
    FILE *f = fopen(filename, "r+");

    fscanf(f, "%d\n", &tests_count);
    tests = new Test[tests_count];
    for(int i = 0; i < tests_count; i++)
    {
		fgets(tests[i].name, 1000, f);
		tests[i].name[strlen(tests[i].name) - 1] = '\0';
		fscanf(f, "%d\n", &tests[i].questions_count);
        tests[i].questions = new Question[tests[i].questions_count];
        for(int j = 0; j < tests[i].questions_count; j++)
        {
			fgets(tests[i].questions[j].text, 1000, f);
			tests[i].questions[j].text[strlen(tests[i].questions[j].text) - 1] = '\0';
            fscanf(f,"%d\n%d\n", &tests[i].questions[j].correct_answer, &tests[i].questions[j].answers_count);
            tests[i].questions[j].answers = new char*[tests[i].questions[j].answers_count];            
            for(int z = 0; z < tests[i].questions[j].answers_count; z++)
            {
                tests[i].questions[j].answers[z] = new char[1000];
				fgets(tests[i].questions[j].answers[z], 1000, f);
				tests[i].questions[j].answers[z][strlen(tests[i].questions[j].answers[z]) - 1] = '\0';
            }
        } 
		fscanf(f, "%d\n", &tests[i].result_count);
		for(int j = 0; j < tests[i].result_count; j++)
		{
			fgets(tests[i].results[j].name, 1000, f);
			tests[i].results[j].name[strlen(tests[i].results[j].name) - 1] = '\0';
			fscanf(f, "%d\n", &tests[i].results[j].result);
		}

    }           
    fclose(f);   
}

DB::~DB()
{
    const char *filename = "base.txt";
    FILE *f = fopen(filename, "w+");
    
    fprintf(f, "%d\n", tests_count);
    for(int i = 0; i < tests_count; i++)
    {
        fprintf(f, "%s\n%d\n", tests[i].name, tests[i].questions_count);
        for(int j = 0; j < tests[i].questions_count; j++)
        {
            fprintf(f, "%s\n%d\n%d\n", tests[i].questions[j].text, tests[i].questions[j].correct_answer,tests[i].questions[j].answers_count);
            for(int z = 0; z < tests[i].questions[j].answers_count; z++)
                fprintf(f, "%s\n", tests[i].questions[j].answers[z]);
        }
		fprintf(f, "%d\n", tests[i].result_count);
		for(int j = 0 ; j < tests[i].result_count; j++)
			fprintf(f, "%s\n%d\n", tests[i].results[j].name, tests[i].results[j].result);
    }
    fclose(f);
}


