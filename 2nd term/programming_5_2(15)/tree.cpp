#include <cstdlib>
#include <cstdio>
#include "tree.h"

Tree::Tree()
{
	root = NULL;
}

void Tree::Add(int value)
{
	Node *cur = root, *cur_parent = NULL;
	while(cur != NULL)
	{
		cur_parent = cur;
		cur = (value < cur->value) ? cur->right : cur->left;
	}
	Node *newnode = new Node(value, cur_parent);
	if(cur_parent == NULL)
		root = newnode;
	else
	{
		if(value < cur_parent->value)
			cur_parent->right = newnode;
		else
			cur_parent->left = newnode;
	}
	fix_tree(newnode);
}

void Tree::fix_tree(Node *node)
{
	while(node->parent != NULL && node->parent->parent != NULL && node->parent->color == RED)
	{
		if(node->parent == node->parent->parent->left)
		{
			Node *uncle = node->parent->parent->right;
			if(uncle && uncle->color == RED)
			{
				node->parent->color = uncle->color = BLACK;
				node->parent->parent->color = RED;
				node = node->parent->parent;
			}
			else
			{
				if(node == node->parent->right)
				{
					node = node->parent;
					left_rotate(node);
				}
				node->parent->color = BLACK;
				node->parent->parent->color = RED;
				right_rotate(node->parent->parent);
			}
		}
		else
		{
			Node *uncle = node->parent->parent->left;
			if(uncle && uncle->color == RED)
			{
				node->parent->color = uncle->color = BLACK;
				node->parent->parent->color = RED;
				node = node->parent->parent;
			}
			else
			{
				if(node == node->parent->left)
				{
					node = node->parent;
					right_rotate(node);
				}
				node->parent->color = BLACK;
				node->parent->parent->color = RED;
				left_rotate(node->parent->parent);
			}
		}
	}
	root->color = BLACK;
}

void Tree::left_rotate(Node *node)
{
	Node *y = node->right, *x = node;
	x->right = y->left;
	if(y->left != NULL)
		y->left->parent = x;
	y->parent = x->parent;
	if(x->parent == NULL)
		root = y;
	else
	{
		if(x == x->parent->left)
			x->parent->left = y;
		else
			x->parent->right = y;
	}
	y->left = x;
	x->parent = y;
}

void Tree::right_rotate(Node *node)
{
	Node *y = node, *x = node->left;
	y->left = x->right;
	if(x->right != NULL)
		x->right->parent = y;
	x->parent = y->parent;
	if(y->parent == NULL)
		root = x;
	else
	{
		if(y->parent->left == y)
			y->parent->left = x;
		else
			y->parent->right = x;
	}
	x->right = y;
	y->parent = x;
}

void Tree::rec_print(SortType st, Node *node)
{
	if(node == NULL)
		return;
	if(st == Ascending)
	{
		rec_print(st, node->left);
		printf("%d ", node->value);
		rec_print(st, node->right);
	}
	else
	{
		rec_print(st, node->right);
		printf("%d ", node->value);
		rec_print(st, node->left);
	}
	

}

void Tree::Print(SortType st)
{
	rec_print(st, root);
}



void Tree::rec_print(Node *node, int level)
{
	if(node->left != NULL)
		rec_print(node->left, level + 1);
	for(int i = 0; i < level; i++)
		printf(" ");
	printf("%d\n", node->value);
	if(node->right != NULL)
		rec_print(node->right, level + 1);
}



void Tree::Print()
{
	rec_print(root, 0);
}