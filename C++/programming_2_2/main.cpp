#include <cstdio>
#include <iostream>
#include <locale>

using namespace std;

enum MenuChoises
{
	INPUT = 1,
	OPTIMAL = 2,
	INFO = 3,
	FEEDBACK = 4,
	EXIT = 5,
	BAD_VALUE = 6
};

struct stats
{
	int minutes_in;
	int minutes_out;
	int minutes_city;
	int sms_count;
	bool used;

	stats()
	{
		used = false;
	}
};

stats data;

struct rate
{
	char *name;
	int month_fee;
	int cost_in;
	int cost_out;
	int cost_sms;

	rate(char *_name, int _month_fee, int _cost_in, int _cost_out, int _cost_sms)
	{
		name = _name;
		month_fee = _month_fee;
		cost_in = _cost_in;
		cost_out = _cost_out;
		cost_sms = _cost_sms;
	}

	int count(stats &stat)
	{
		return month_fee + cost_in * stat.minutes_in + cost_out * stat.minutes_out + cost_out * stat.minutes_city + cost_sms * stat.sms_count;
	}

	void print()
	{
		cout << name << endl;
		cout << "Month fee: " << month_fee << endl;
		cout << "Cost to MTS: " << cost_in << endl;
		cout << "Cost to other providers: " << cost_out << endl;
		cout << "Cost SMS: " << cost_sms << endl;
		cout << endl;
	}
};

const int RATES_COUNT = 4;
rate rates[RATES_COUNT] = {rate("Всё для общения", 9900, 75, 295, 120),
rate("Отличный", 9900, 60, 280, 120),
rate("Будь практичнее", 6000, 40, 280, 120),
rate("Детский", 0, 75, 350, 75)};


int menu()
{
	cout << "1 - Input data" << endl;
	cout << "2 - Calculate optimal rate" << endl;
	cout << "3 - Information" << endl;
	cout << "4 - Feedback" << endl;
	cout << "5 - Exit" << endl;
	cout << "? ";

	int value;
	cin >> value;

	return value;
}

void read_param(char *param_name, int &value)
{
	cout << param_name << ":";
	cin >> value;
}

void input()
{
	read_param("Minutes on MTS", data.minutes_in);
	read_param("Munutes on other", data.minutes_out);
	read_param("Minutes on city phone", data.minutes_city);
	read_param("SMS count", data.sms_count);
	data.used = true;
}

void print_info()
{
	for(int i = 0; i < RATES_COUNT; i++)
		rates[i].print();	
}

int find_optimal()
{
	int min_index, min_value = 99999999;
	for(int i = 0; i < RATES_COUNT; i++)
		if(rates[i].count(data) < min_value)
		{
			min_value = rates[i].count(data);
			min_index = i;
		}
	return min_index;
}

int main(int argc, char **argv)
{
	setlocale(LC_ALL, "");
	int user_choise;
	while((user_choise = menu()) != MenuChoises::EXIT)
	{
		int result;
		switch(user_choise)
		{
			case MenuChoises::INPUT:
				input();
				break;
			case MenuChoises::OPTIMAL:
				if(data.used)
				{	
					result = find_optimal();
					cout << rates[result].name << " - " << rates[result].count(data);
				}
				else
					cout << "No data";
				break;
			case MenuChoises::INFO:
				print_info();
				break;
			case MenuChoises::FEEDBACK:
				cout << "Звоните 411";
				break;
			default:
				cout << "Incorrect choise" << endl;
				break;
		}
		cout << endl << endl;
	}
	return 0;
}