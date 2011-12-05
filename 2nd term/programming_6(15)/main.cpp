#include <cstdio>
#include <iostream>
#include <string>

using namespace std;

struct Item
{
	int code;
	char *name;
	int price;

	Item()
	{
		name = new char[1000];
	}

	Item(int _code, char* _name, int _price)
	{
		name = new char[1000];
		code = _code;
		strcpy(name, _name);
		price = _price;
	}
};

struct Card
{
	int serial;
	int discount;

	Card(){}
	Card(int _serial, int _discount)
	{
		serial = _serial;
		discount = _discount;
	}
};

struct HistoryItem
{
	int serial;
	int count;
};

struct History
{
	HistoryItem *items;
	Card card;
	int items_count;
	bool card_exist;

	History()
	{
		items_count = 0;
		items = new HistoryItem[1000];
	}

};


class DB
{
public:
	History *history;
	Item *items;
	Card *cards;

	int items_count;
	int history_count;
	int cards_count;

	bool find_item(Item &item, int code)
	{
		for(int i = 0; i < items_count; i++)
			if(items[i].code == code)
			{
				item = items[i];
				return true;
			}
		return false;
	}

	int get_discount(int serial)
	{
		for(int i = 0; i < cards_count; i++)
			if(cards[i].serial == serial)
				return cards[i].discount;
		return -1;
	}

	DB()
	{
		history = new History[1000];
		items = new Item[1000];
		cards = new Card[1000];

		items_count = history_count = cards_count = 0;
		FILE *f = fopen("base.txt", "r+");

		fscanf(f, "%d", &items_count);
		for(int i = 0; i < items_count; i++)
		{
			fscanf(f, "%d %d", &items[i].code, &items[i].price);
			fgets(items[i].name, 1000, f);
			items[i].name[strlen(items[i].name) - 1] = '\0';
		}

		fscanf(f, "%d", &history_count);
		for(int i = 0; i < history_count; i++)
		{
			fscanf(f, "%d", &history[i].items_count);
			for(int j = 0; j < history[i].items_count; j++)
				fscanf(f, "%d %d", &history[i].items[j].serial, &history[i].items[j].count);
			fscanf(f, "%d", &history[i].card_exist);
			if(history[i].card_exist)
				fscanf(f, "%d %d", &history[i].card.serial, &history[i].card.discount);
		}
		fscanf(f, "%d", &cards_count);
		for(int i = 0; i < cards_count; i++)
			fscanf(f, "%d %d", &cards[i].serial, &cards[i].discount);
		fclose(f);
	}

	~DB()
	{
		FILE *f = fopen("base.txt", "w+");
		
		fprintf(f, "%d\n", items_count);
		for(int i = 0; i < items_count; i++)
			fprintf(f, "%d %d\n%s\n", items[i].code, items[i].price, items[i].name);
		fprintf(f, "%d\n", history_count);
		for(int i = 0; i < history_count; i++)
		{
			fprintf(f, "%d\n", history[i].items_count);
			for(int j = 0; j < history[i].items_count; j++)
				fprintf(f, "%d %d\n", history[i].items[j].serial, history[i].items[j].count);
			if(history[i].card_exist)
				fprintf(f, "1\n%d %d", history[i].card.serial, history[i].card.discount);
			else 
				fprintf(f, "0\n");
		}
		fprintf(f, "%d\n", cards_count);
		for(int i = 0; i < cards_count; i++)
			fprintf(f, "%d %d\n", cards[i].serial, cards[i].discount);
		fclose(f);

		delete history;
		delete items;
	}
};

DB db;

int main()
{
	int cmd, cmd2, cmd3, code, count, sum, discount, price;
	char ch;
	char *name = new char[1000];
	bool found;
	Item item;
	while(true)
	{
		printf("1 - New customer\n");
		printf("2 - Items\n");
		printf("3 - History\n");
		printf("4 - Cards\n");
		printf("5 - Exit\n? ");
		scanf("%d", &cmd);
		if(cmd == 1)
		{
			History *hi = new History();
			while(true)
			{
				printf("1 - Add item\n");
				printf("2 - Show items\n");
				printf("3 - Calculate\n");
				printf("4 - Back\n? ");
				scanf("%d", &cmd2);
				if(cmd2 == 1)
				{
					printf("Code: ");
					scanf("%d", &code);
					printf("Count: ");
					scanf("%d", &count);
					found = db.find_item(item, code);
					if(!found)
						printf("Incorrect code!\n\n");
					else 
					{
						hi->items[hi->items_count].serial = code;
						hi->items[hi->items_count++].count = count;
					}
				}
				else if(cmd2 == 2)
				{
					for(int i = 0; i < hi->items_count; i++)
					{
						found = db.find_item(item, hi->items[i].serial);
						if(!found)
							printf("Item not found\n\n");
						else
							printf("Item Code: %d\nCount:%d\nName: %s\nPrice: %d\n\n", item.code, hi->items[i].count, item.name, item.price);
					}
				}
				else if(cmd2 == 3)
				{
					sum = 0;
					for(int i = 0; i < hi->items_count; i++)
					{
						found = db.find_item(item, hi->items[i].serial);
						sum += item.price * hi->items[i].count;
					}
					printf("The total ammount is %d\n", sum);
					while(true)
					{
						printf("\nDo you have discont card(y\n)? ");
						scanf("%c", &ch);
						if(ch == 'y')
						{
							printf("\nSerial: ");
							scanf("%d", &code);
							discount = db.get_discount(code);
							if(discount == -1)
							{
								printf("Card no found. Try again");
								continue;
							}
							else
								sum = sum - sum * discount / 100;
							printf("The total ammount is %d\n", sum);
						}
						else if(ch == 'n')
							break;
					}
					db.history[db.history_count++] = *hi;
				}
				else if(cmd2 == 4)
					break;
				else
					printf("Incorrect choise. Try again\n\n");
			}
		}
		else if(cmd == 2)
		{
			while(true)
			{
				printf("1 - Add Item\n");
				printf("2 - Back\n\n? ");
				scanf("%d", &cmd2);
				if(cmd2 == 1)
				{
					printf("Code: ");
					scanf("%d", &code);
					printf("Name: ");
					cin.get();cin.get(name, 1000);
					printf("Price: ");
					scanf("%d", &price);

					db.items[db.items_count++] = Item(code, name, price);
				}
				else if(cmd2 == 2)
					break;
				else 
					printf("Incorrect choise. Try again\n\n");
			}
		}
		else if(cmd == 4)
		{
			while(true)
			{
				printf("1 - Add card\n");
				printf("2 - Back\n\n? ");
				scanf("%d", &cmd2);
				if(cmd2 == 1)
				{
					printf("Code: ");
					scanf("%d", &code);
					printf("Discount: ");
					scanf("%d", &discount);

					db.cards[db.cards_count++] = Card(code, discount);
				}
				else if(cmd2 == 2)
					break;
				else
					printf("Incorrect choise. Try again\n\n");
			}
		}
		else if(cmd == 5)
			break;
		else
			printf("Incorrect choise. Try again\n\n");
	}
	delete name;
	return 0;
}