#include <cstdio>
#include <cstdlib>
#include <conio.h>
#include "menu.h"

MenuManager menu_manager;

void edit_tests()
{
	menu_manager.show("EditTests");
}

void add_test(){}
void run_test(){}
void stats(){}
void set_password(){}
void delete_test(){}


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
	MenuItem *set_password_mi = new MenuItem("Set Password", set_password);
	MenuItem *back_mi = new MenuItem("Back", back);
	MenuItem *exit_mi = new MenuItem("Exit", exit);

	Menu *main_menu = new Menu("Main");
	main_menu->add(edit_tests_mi);
	main_menu->add(run_test_mi);
	main_menu->add(stats_mi);
	main_menu->add(set_password_mi);
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

	getch();
	return 0;
}