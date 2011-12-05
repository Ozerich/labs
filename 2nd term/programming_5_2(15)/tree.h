#ifndef TREE_H
#define TREE_H

enum Color {RED, BLACK};
enum SortType {Ascending, Descending};

struct Node
{
	Node *left;
	Node *right;
	Node *parent;
	int value;
	Color color;

	Node(int val, Node *par)
	{
		value = val;
		left = right = NULL;
		color = RED;
		parent = par;
	}
};

class Tree
{
private:
	Node *root;
	void fix_tree(Node *node);
	void left_rotate(Node *node);
	void right_rotate(Node *right);
	void rec_print(SortType st, Node *node);
	void rec_print(Node *node, int level);
public:
	Tree();
	void Add(int value);
	void Print(SortType st);
	void Print();
};

#endif