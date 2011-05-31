#include <cstdio>
#include <iostream>
#include <string>
#include <cstdlib>
#include "menu.h"
#include "db.h"

using namespace std;

MenuManager menu_manager;
DB db;

void edit_tests()
{
    for(int i = 0; i < db.tests_count; i++)
        printf("%d - %s\n", i + 1, db.tests[i].name);
    printf("\n");
	menu_manager.show("EditTests");
}

void add_test()
{
    Test test;
    
    printf("Test name: ");
    scanf("%s", test.name);
    printf("Questions count: ");
    scanf("%d", &test.questions_count);
    test.questions = new Question[test.questions_count];
    for(int i = 0; i < test.questions_count; i++)
    {
        printf("%d question:\nName:", i + 1);
        test.questions[i].text = new char[1000];
		cin.get();
		cin.get(test.questions[i].text, 1000);
        printf("Answers count: ");
        scanf("%d", &test.questions[i].answers_count);
        test.questions[i].answers  = new char*[test.questions[i].answers_count];
        for(int j = 0; j < test.questions[i].answers_count; j++)
        {
            printf("%d answer: ", j + 1);
            test.questions[i].answers[j] = new char[1000];
			cin.get();
			cin.get(test.questions[i].answers[j], 1000);
        }
        printf("Correct answer: ");
        scanf("%d", &test.questions[i].correct_answer);   
    }
    
    db.tests[db.tests_count++] = test;
    printf("Test is added\n");
    
    menu_manager.clear();
    menu_manager.show("Main");
    
}
    


void run_test()
{
	char *user_name = new char[1000];
	printf("Your name: ");
	cin.get();
	cin.get(user_name, 1000);
    for(int i = 0; i < db.tests_count; i++)
        printf("%d - %s\n", i + 1, db.tests[i].name);
    printf("\n");
    int index; 
    printf("Test index: ");
    scanf("%d", &index);
	printf("\n");
    
    int good_count = 0;
    int user_answer;
    
    Test test = db.tests[index - 1];
    for(int i = 0; i < test.questions_count; i++)
    {
        printf("Question: %s\n", test.questions[i].text);
        for(int j = 0; j < test.questions[i].answers_count; j++)
            printf("Answer %d: %s\n", j + 1, test.questions[i].answers[j]);
        printf("\nYour answer: ");
        scanf("%d", &user_answer);
        if(user_answer == test.questions[i].correct_answer)
            good_count++;
        printf("\n");
    }
    
    printf("Test completed. Your result: %d correct answers", good_count++);
	db.tests[index - 1].AddResult(user_name, good_count);
    printf("\n\n");
    menu_manager.clear();
    menu_manager.show("Main");
}



void stats()
{
	for(int i = 0; i < db.tests_count; i++)
        printf("%d - %s\n", i + 1, db.tests[i].name);
    printf("\n");
    int index; 
    printf("Test index: ");
    scanf("%d", &index);
	UserResult *results = db.tests[index - 1].results;
	for(int i = 0; i < db.tests[index - 1].result_count; i++)
		printf("%s - %d\n", results[i].name, results[i].result);
    printf("\n\n");
    menu_manager.clear();
    menu_manager.show("Main");
}

void delete_test()
{
    int index;
    printf("Test number: ");
    scanf("%d", &index);
    db.deleteTest(index);
    
    printf("Test deleted");
    menu_manager.clear();
    menu_manager.show("EditMenu");
}


void back()
{
	menu_manager.back();
}

void exit()
{
	exit(0);
}

int main()
{
	MenuItem *edit_tests_mi = new MenuItem("Edit Tests", edit_tests);
	MenuItem *add_test_mi = new MenuItem("Add Test", add_test);
	MenuItem *delete_test_mi = new MenuItem("Delete Test", delete_test);
	MenuItem *run_test_mi = new MenuItem("Run Test", run_test);
	MenuItem *stats_mi = new MenuItem("Statictics", stats);
	MenuItem *back_mi = new MenuItem("Back", back);
	MenuItem *exit_mi = new MenuItem("Exit", exit);

	Menu *main_menu = new Menu("Main");
	main_menu->add(edit_tests_mi);
	main_menu->add(run_test_mi);
	main_menu->add(stats_mi);
	main_menu->add(exit_mi);

	Menu *edit_test_menu = new Menu("EditTests");
	edit_test_menu->add(add_test_mi);
	edit_test_menu->add(delete_test_mi);
	edit_test_menu->add(back_mi);
	edit_test_menu->add(exit_mi);

	menu_manager.add(edit_test_menu);
	menu_manager.add(main_menu);

	menu_manager.show("Main");

	menu_manager.run();

	return 0;
}
