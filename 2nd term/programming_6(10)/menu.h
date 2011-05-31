#ifndef MENU_H
#define MENU_H

#include "string.h"

class MenuItem
{
public:
    char* name;
	void (*onClick)(void);
	MenuItem(const char *, void (*)(void));
	MenuItem();
};

MenuItem::MenuItem(const char *name_, void (*func)(void))
{
	name = new char[100];
	strcpy(name, name_);
	onClick = func;
}

MenuItem::MenuItem(){}


class Menu
{
public:
	MenuItem *menu_items;
	int items_count;
	void show();
	char *id;
	Menu(const char *);
	Menu();
	
	void add(MenuItem *);
};

Menu::Menu(){}

Menu::Menu(const char *id_)
{
	menu_items = new MenuItem[100];
	items_count = 0;
	id = new char[100];
	strcpy(id, id_);
}

void Menu::add(MenuItem *mi)
{
	menu_items[items_count++] = *mi;
}

void Menu::show()
{
	for(int i = 0; i < items_count; i++)
		printf("%d - %s\n", i + 1, menu_items[i].name);
	printf("? ");
}


class MenuManager
{
public:
	void show(const char *id);
	Menu *menu_data;
	int menu_data_count;
	Menu *stack;
	int stack_len;
	void back();

	void add(Menu *menu);
	MenuManager();

	void run();
	void clear();
};


void MenuManager::clear()
{
    stack_len = 0;
}

MenuManager::MenuManager()
{
	menu_data = new Menu[1000];
	stack = new Menu[1000];
	menu_data_count = stack_len = 0;
}

void MenuManager::show(const char *id)
 {
	for(int i = 0; i < menu_data_count; i++)
		if(strcmp(menu_data[i].id, id) == 0)
		{
			stack[stack_len++] = menu_data[i];
			menu_data[i].show();
			break;
		}
}

void MenuManager::add(Menu *menu)
{
	menu_data[menu_data_count++] = *menu;
}

void MenuManager::run()
{
	int key;
	while(true)
	{
		scanf("%d", &key);
		Menu menu = stack[stack_len - 1];
		if(key > menu.items_count || key <= 0)
			printf("Incorrect choise\n");
		else
			menu.menu_items[key - 1].onClick();
	}
}

void MenuManager::back()
{
	stack[--stack_len - 1].show();
}

#endif
