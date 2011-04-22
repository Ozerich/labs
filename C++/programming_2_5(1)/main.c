#include <stdio.h>
#include <stdlib.h>

typedef struct node{
    int value;
    int used;
    struct node *next;
    struct node *prev;
} NODE;

typedef struct list{
    struct node *begin;
    struct node *end;
} LIST;

int t, n;

int GetIndex(LIST *list, NODE *node)
{
    NODE *cur_node = list->begin;
    int index = 0;
    while(cur_node != 0)
    {
        index++;
        if(cur_node == node)
            return index;
        cur_node = cur_node->next;
    }
    return -1;
}

void AddList(LIST *list, int value)
{
    if(list->begin == 0)
    {
        list->begin = (NODE *)malloc(sizeof(NODE));
        list->begin->next = list->begin->prev = 0;
        list->begin->used = 0;
        list->begin->value = value;
        list->end = list->begin;
        return ;
    }
    NODE *node = (NODE *)malloc(sizeof(NODE));
    node->value = value;
    node->used = 0;
    node->next = 0;
    node->prev = list->end;
    list->end->next = node;
    list->end = node;
}

NODE* FindMin(LIST *list)
{
    NODE *node = list->begin;
    int min_value = 9999999;
    NODE *min_node = 0;
    while(node != 0)
    {
        if(node->used == 0 && node->value < min_value)
        {
            min_value = node->value;
            min_node = node;
        }
        node = node->next;
    }
    if(min_node != 0)
        min_node->used = 1;
    return min_node;
}

void freeList(LIST *list)
{
    NODE *node = list->begin, *cur;
    while(node != 0)
    {
        cur = node;
        node = node->next;
        free(cur);
    }
    free(list);
}

int main()
{
    int i,n, t;
    LIST *list = (LIST *)malloc(sizeof(LIST));
    FILE *f = fopen("input", "r");
    fscanf(f, "%d", &n);
    for(i = 0; i < n; i++)
    {
        fscanf(f, "%d", &t);
        AddList(list, t);
    }
    fscanf(f, "%d", &t);
    int cur = 0;
    while(1)
    {
        NODE *node = FindMin(list);
        if(node == 0)break;
        if(cur + node->value > t)
            break;
        cur += node->value;
        int index = GetIndex(list, node);
        printf("%d ", GetIndex(list, node));
    }
    freeList(list);
    getchar();
    return 0;
}
