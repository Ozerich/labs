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


class Test
{
public:
    Question *questions;
    int questions_count;
    char *name;
    
    Test(char *name_)
    {
        name = new char[100];
        strcpy(name, name_);
        questions_count = 0;
    }
    
    Test()
    {
        name = new char[100];
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
        fscanf(f, "%s\n%d\n", tests[i].name, &tests[i].questions_count);
        tests[i].questions = new Question[tests[i].questions_count];
        for(int j = 0; j < tests[i].questions_count; j++)
        {
            fscanf(f,"%s\n%d\n%d\n", tests[i].questions[j].text, &tests[i].questions[j].correct_answer, &tests[i].questions[j].answers_count);
            tests[i].questions[j].answers = new char*[tests[i].questions[j].answers_count];            
            for(int z = 0; z < tests[i].questions[j].answers_count; z++)
            {
                tests[i].questions[j].answers[z] = new char[1000];
                fscanf(f,"%s\n", tests[i].questions[j].answers[z]);
            }
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
    }
    fclose(f);
}


